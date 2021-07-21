using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            ApplySettingsToDefs();
        }

        public static void ApplySettingsToDefs()
        {
            if (SquirtingElephantSettings.Settings.BiocodeData == null) { SquirtingElephantSettings.Settings.CreateBiocodeData(); }

            List<PawnKindDef> allPawnKindDefs = DefDatabase<PawnKindDef>.AllDefsListForReading;
            foreach (PawnKindDef pkd in allPawnKindDefs)
            {
                BiocodeData biocodeData = SquirtingElephantSettings.Settings.BiocodeData.FirstOrDefault(d => d.DefName == pkd.defName);
                pkd.biocodeWeaponChance = biocodeData == null ? SquirtingElephantSettings.Settings.BCD_Other.BiocodeChance : biocodeData.BiocodeChance;
            }
        }
    }
}
