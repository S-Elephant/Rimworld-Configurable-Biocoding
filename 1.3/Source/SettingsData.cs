using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class SettingsData : ModSettings
    {
        public readonly List<BiocodeData> BiocodeData = new List<BiocodeData>();
        private static readonly HashSet<string> _allRacesThatUseBiocoding = new HashSet<string>
        {
            "Human",
        };

        public SettingsData()
        {
            IfNoDataThenCreateIt();
        }

        public override void ExposeData()
        {
            IfNoDataThenCreateIt();
            BiocodeData.ForEach(d => d.ExposeData());
        }

        private void CreateBiocodeData(List<PawnKindDef> allPawnKindDefsReadonly)
        {
            BiocodeData.Clear();
            foreach (PawnKindDef pkd in allPawnKindDefsReadonly)
                BiocodeData.Add(new BiocodeData(pkd.defName, pkd.defName.Replace('_', ' ')));
        }

        public void IfNoDataThenCreateIt()
        {
            if (BiocodeData.Count == 0)
                CreateBiocodeData(GetAllValidPawnKindDefs());
        }

        public static List<PawnKindDef> GetAllValidPawnKindDefs()
        {
            return DefDatabase<PawnKindDef>.AllDefsListForReading
                .Where(pkd => _allRacesThatUseBiocoding.Contains(pkd.race.defName))
                .ToList();
        }
    }
}
