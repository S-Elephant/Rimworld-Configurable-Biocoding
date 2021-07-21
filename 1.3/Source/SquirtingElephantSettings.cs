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
        private const float PADDING_HORIZONTAL = 10f;
        private const float SPACING_HORIZONTAL = 20f;
        private const float ROW_HEIGHT = 32f;
        private float SetAllChance = Consts.VANILLA_BIOCODE_CHANCE;

        public static TableData Table = new TableData(new Vector2(PADDING_HORIZONTAL, 0f), new Vector2(SPACING_HORIZONTAL, 10f),
            new float[] { 310f, 340f },
            new float[] { ROW_HEIGHT },
            2);

        // Scroll settings.
        private Vector2 ScrollPosition = Vector2.zero;
        private Rect ViewRect = new Rect(0.0f, 0.0f, 100.0f, 200.0f);

        #endregion

        public SquirtingElephantSettings(ModContentPack content) : base(content) { Settings = GetSettings<SettingsData>(); }

        public override void DoSettingsWindowContents(Rect settingsWindowSizeRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(settingsWindowSizeRect);

            CreateAllSetting();

            Widgets.Label(new Rect(PADDING_HORIZONTAL, 20f + ROW_HEIGHT, settingsWindowSizeRect.width, ROW_HEIGHT), "SECBC_SettingInfo".TC());
            Rect scrollViewRect = new Rect(
                    settingsWindowSizeRect.x + PADDING_HORIZONTAL,
                    settingsWindowSizeRect.y,
                    settingsWindowSizeRect.width - 2 * PADDING_HORIZONTAL - 2 * SPACING_HORIZONTAL,
                    settingsWindowSizeRect.height);
            Widgets.BeginScrollView(new Rect(
                    PADDING_HORIZONTAL,
                    SCROLL_AREA_OFFSET_TOP,
                    settingsWindowSizeRect.width - 2 * PADDING_HORIZONTAL - SPACING_HORIZONTAL,
                    settingsWindowSizeRect.height - SCROLL_AREA_OFFSET_TOP
                ),
                ref ScrollPosition,
                scrollViewRect);

            CreateHeaders();
            CreateFields();
            ls.GetRect(Table.Bottom); // Ensures the scrollview works.
            Widgets.EndScrollView();
            ls.End();

            Main.ApplySettingsToDefs();
        }

        private void CreateAllSetting()
        {
            string bufferAllChance = SetAllChance.ToString();
            Widgets.TextFieldNumericLabeled(new Rect(10f, 10f, 300f, ROW_HEIGHT), "SECBC_SetAllFields".TC() + " ", ref SetAllChance, ref bufferAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            float count = Widgets.HorizontalSlider(new Rect(310f, 10f, 300f, ROW_HEIGHT), SetAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            if (count != SetAllChance)
                SetAllChance = count;
            if (Widgets.ButtonText(new Rect(610f, 10f, 120f, ROW_HEIGHT), "SECBC_ApplyAll".TC()))
                Settings.SetAllValues(SetAllChance);
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
