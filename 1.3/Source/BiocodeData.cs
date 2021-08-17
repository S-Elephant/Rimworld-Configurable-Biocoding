using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class BiocodeData : IExposable
    {
        public float BiocodeChance;
        public readonly string Label;
        public string DefName;

        public BiocodeData(string defName, string label, float biocodeChance = Consts.VANILLA_BIOCODE_CHANCE)
        {
            DefName = defName;
            Label = label;
            BiocodeChance = biocodeChance;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref BiocodeChance, $"SECBC_{DefName}_BiocodeChance", Consts.VANILLA_BIOCODE_CHANCE, true);
        }
    }
}
