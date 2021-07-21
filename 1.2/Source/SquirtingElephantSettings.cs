using SquirtingElephant.Helpers;
using UnityEngine;
using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class SquirtingElephantSettings : Mod
    {
        #region Fields
        
        public static SettingsData Settings;

        private const float SCROLL_AREA_OFFSET_TOP = 100f;
        private const float ROW_HEIGHT = 32f;
        private float SetAllChance = Consts.VANILLA_BIOCODE_CHANCE;

        public static TableData Table = new TableData(new Vector2(10f, 0f), new Vector2(20f, 10f),
            new float[] { 340f, 370f },
            new float[] { ROW_HEIGHT },
            2);

        // Scroll settings.
        private Vector2 ScrollPosition = Vector2.zero;
        private Rect ViewRect = new Rect(0.0f, 0.0f, 100.0f, 200.0f);
        
        #endregion

        public SquirtingElephantSettings(ModContentPack content) : base(content) { Settings = GetSettings<SettingsData>(); }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);

            CreateTheAllSetting();

            Widgets.Label(new Rect(10f, 20f + ROW_HEIGHT, inRect.width, ROW_HEIGHT), "SECBC_SettingInfo".TC());

            ls.BeginScrollView(new Rect(0, SCROLL_AREA_OFFSET_TOP, inRect.width, inRect.height - SCROLL_AREA_OFFSET_TOP), ref ScrollPosition, ref ViewRect);

            CreateHeaders();
            CreateFields();
            ls.GetRect(Table.Bottom); // Ensures the scrollview works.

            ls.EndScrollView(ref ViewRect);
            ls.End();

            Main.ApplySettingsToDefs();
        }

        private void CreateTheAllSetting()
        {
            string bufferAllChance = SetAllChance.ToString();
            Widgets.TextFieldNumericLabeled(new Rect(10f, 10f, 300f, ROW_HEIGHT), "SECBC_SetAllFields".TC() + " ", ref SetAllChance, ref bufferAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            float count = Widgets.HorizontalSlider(new Rect(310f, 10f, 300f, ROW_HEIGHT), SetAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            if (count != SetAllChance)
            {
                SetAllChance = count;
            }
            if (Widgets.ButtonText(new Rect(610f, 10f, 120f, ROW_HEIGHT), "SECBC_ApplyAll".TC()))
            {
                Settings.SetAllValues(SetAllChance);
            }
        }

        private void CreateHeaders()
        {
            Widgets.Label(Table.GetHeaderRect(0), "SECBC_HeaderPawnKindName".TC());
            Widgets.Label(Table.GetHeaderRect(1), "SECBC_HeaderBiocodeChance".TC());
        }

        private void CreateFields()
        {
            int rowIdx = 1;
            foreach (BiocodeData biocodeData in Settings.BiocodeData)
                MakeInputs(rowIdx++, biocodeData.SettingsTranslationKey, ref biocodeData.BiocodeChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);

            MakeInputs(rowIdx++, Settings.BCD_Other.SettingsTranslationKey, ref Settings.BCD_Other.BiocodeChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
        }

        private void MakeInputs(int rowIdx, string translationKey, ref float setting, float min, float max)
        {
            string buffer = setting.ToString();
            Widgets.TextFieldNumericLabeled(Table.GetFieldRect(0, rowIdx), translationKey.TC() + " ", ref setting, ref buffer, min, max);

            float count = Widgets.HorizontalSlider(Table.GetFieldRect(1, rowIdx), setting, min, max);
            if (count != setting)
                setting = count;
        }

        public override string SettingsCategory() => "SECBC_SettingsCategory".Translate();
    }
}
