using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class BiocodeData : IExposable
    {
        public float BiocodeChance;
        public readonly string SettingsTranslationKey;
        public string DefName;

        public BiocodeData(string defName, string settingsTranslationKey, float biocodeChance = Consts.VANILLA_BIOCODE_CHANCE)
        {
            DefName = defName;
            SettingsTranslationKey = settingsTranslationKey;
            BiocodeChance = biocodeChance;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref DefName, "SECBC_DefName", null, true);
            Scribe_Values.Look(ref BiocodeChance, "SECBC_BiocodeChance", Consts.VANILLA_BIOCODE_CHANCE);
        }
    }
}
