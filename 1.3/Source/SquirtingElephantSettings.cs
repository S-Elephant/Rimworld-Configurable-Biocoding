using System.Collections.Generic;
using System.Linq;
using SquirtingElephant.Helpers;
using UnityEngine;
using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class SquirtingElephantSettings : Mod
    {
        #region Fields

        public static SettingsData Settings;
        private static readonly Dictionary<string, BufferContainer> _buffers = new Dictionary<string, BufferContainer>();

        private const float SCROLL_AREA_OFFSET_TOP = 100f;
        private const float PADDING_HORIZONTAL = 10f;
        private const float SPACING_HORIZONTAL = 20f;
        private const float ROW_HEIGHT = 32f;
        private const float ROW_SPACING = 10f;
        private const float EXTRA_HEIGHT = ROW_HEIGHT;

        public static TableData Table = new TableData(new Vector2(PADDING_HORIZONTAL, 0f), new Vector2(SPACING_HORIZONTAL, 10f),
            new float[] { 310f, 340f },
            new float[] { ROW_HEIGHT },
            2);

        private Vector2 _scrollPosition = Vector2.zero;
        private float _setAllChance = Consts.VANILLA_BIOCODE_CHANCE;

        #endregion

        public SquirtingElephantSettings(ModContentPack content) : base(content)
        {
            Settings = GetSettings<SettingsData>();
            if (Settings.BiocodeData.Count == 0)
            {
                Settings.IfNoDataThenCreateIt();
                Settings.ExposeData();
            }
        }

        private static float GetScrollViewHeight() => Settings.BiocodeData.Count * (ROW_HEIGHT + ROW_SPACING) + EXTRA_HEIGHT;

        public void SetAllBuffers(string newFloatValueAsString)
        {
            foreach (BufferContainer bufferContainer in _buffers.Values)
                bufferContainer.Buffer = newFloatValueAsString;
        }

        public static void ApplySettings()
        {
            foreach (PawnKindDef pkd in SettingsData.GetAllValidPawnKindDefs())
            {
                BiocodeData bioCodeData = Settings.BiocodeData.FirstOrDefault(d => d.DefName == pkd.defName);
                pkd.biocodeWeaponChance = bioCodeData == null ? Consts.VANILLA_BIOCODE_CHANCE : bioCodeData.BiocodeChance;
            }
        }

        public void SetAllValues(float newBiocodeChance)
        {
            Settings.BiocodeData.ForEach(d => d.BiocodeChance = newBiocodeChance);
            SetAllBuffers(newBiocodeChance.ToString());
        }

        public override void DoSettingsWindowContents(Rect settingsWindowSizeRect)
        {
            Settings.IfNoDataThenCreateIt();
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(settingsWindowSizeRect);

            CreateAllSetting();
            
            Widgets.Label(new Rect(PADDING_HORIZONTAL, 20f + ROW_HEIGHT, settingsWindowSizeRect.width, ROW_HEIGHT), "SECBC_SettingInfo".TC());
            
            #region Initialize scroll area
            Rect scrollViewRect = new Rect(
                    settingsWindowSizeRect.x + PADDING_HORIZONTAL,
                    settingsWindowSizeRect.y,
                    settingsWindowSizeRect.width - 2 * PADDING_HORIZONTAL - 2 * SPACING_HORIZONTAL,
                    GetScrollViewHeight());
            Widgets.BeginScrollView(new Rect(
                    PADDING_HORIZONTAL,
                    SCROLL_AREA_OFFSET_TOP,
                    settingsWindowSizeRect.width - 2 * PADDING_HORIZONTAL - SPACING_HORIZONTAL,
                    settingsWindowSizeRect.height - SCROLL_AREA_OFFSET_TOP
                ),
                ref _scrollPosition,
                scrollViewRect);
            #endregion

            CreateHeaders();
            CreateFields();

            Widgets.EndScrollView();
            ls.End();

            Main.ApplySettingsToDefs();
        }

        private void CreateAllSetting()
        {
            string bufferAllChance = _setAllChance.ToString();
            Widgets.TextFieldNumericLabeled(new Rect(10f, 10f, 300f, ROW_HEIGHT), "SECBC_SetAllFields".TC() + " ", ref _setAllChance, ref bufferAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            float count = Widgets.HorizontalSlider(new Rect(340f, 10f, 260f, ROW_HEIGHT), _setAllChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
            if (count != _setAllChance)
                _setAllChance = count;
            if (Widgets.ButtonText(new Rect(620f, 10f, 120f, ROW_HEIGHT), "SECBC_ApplyAll".TC()))
                SetAllValues(_setAllChance);
        }

        private static void CreateHeaders()
        {
            Widgets.Label(Table.GetHeaderRect(0), "SECBC_HeaderPawnKindName".TC());
            Widgets.Label(Table.GetHeaderRect(1), "SECBC_HeaderBiocodeChance".TC());
        }

        private static void CreateFields()
        {
            int rowIdx = 1;
            foreach (BiocodeData biocodeData in Settings.BiocodeData)
                MakeInputs(rowIdx++, biocodeData.Label, biocodeData.DefName, ref biocodeData.BiocodeChance, Consts.BIOCODE_CHANCE_MIN, Consts.BIOCODE_CHANCE_MAX);
        }

        private static void MakeInputs(int rowIdx, string label, string defName, ref float setting, float min, float max)
        {
            if (!_buffers.ContainsKey(defName))
                _buffers.Add(defName, new BufferContainer());

            Widgets.TextFieldNumericLabeled(Table.GetFieldRect(0, rowIdx), $"{label} ", ref setting, ref _buffers[defName].Buffer, min, max);

            float newValue = Widgets.HorizontalSlider(Table.GetFieldRect(1, rowIdx), setting, min, max);
            if (newValue != setting)
            {
                setting = newValue;
                _buffers[defName].Buffer = setting.ToString();
            }
        }

        public override string SettingsCategory() => "SECBC_SettingsCategory".Translate();
    }

    internal class BufferContainer
    {
        public string Buffer = Consts.VANILLA_BIOCODE_CHANCE.ToString();
    }
}
