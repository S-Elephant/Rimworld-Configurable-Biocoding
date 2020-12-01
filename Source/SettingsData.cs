using System.Collections.Generic;
using Verse;

namespace SquirtingElephant.ConfigurableBiocoding
{
    public class SettingsData : ModSettings
    {
        public List<BiocodeData> BiocodeData = null;

        /// <summary>
        /// The Biocode data to use for everything else.
        /// </summary>
        public BiocodeData BCD_Other = null;

        public override void ExposeData()
        {
            if (BiocodeData == null) { CreateBiocodeData(); }

            BiocodeData.ForEach(d => d.ExposeData());
            BCD_Other.ExposeData();
        }

        public void CreateBiocodeData()
        {
            BiocodeData = new List<BiocodeData>()
            {
                // Mercenaries from PawnKinds_Mercenary.xml
                new BiocodeData("Grenadier_Destructive", "SECBC_Grenadier_Destructive"),
                new BiocodeData("Grenadier_EMP", "SECBC_Grenadier_EMP"),
                new BiocodeData("Grenadier_Smoke", "SECBC_Grenadier_Smoke"),

                new BiocodeData("Mercenary_Gunner", "SECBC_Mercenary_Gunner"),
                new BiocodeData("Mercenary_Sniper", "SECBC_Mercenary_Sniper"),
                new BiocodeData("Mercenary_Slasher", "SECBC_Mercenary_Slasher"),
                new BiocodeData("Mercenary_Heavy", "SECBC_Mercenary_Heavy"),
                new BiocodeData("PirateBoss", "SECBC_PirateBoss"),
                new BiocodeData("Mercenary_Elite", "SECBC_Mercenary_Elite"),
                
                // Pirates from PawnKinds_Pirate.xml
                new BiocodeData("Drifter", "SECBC_Drifter"),
                new BiocodeData("Scavenger", "SECBC_Scavenger"),
                new BiocodeData("Thrasher", "SECBC_Thrasher"),
                new BiocodeData("Pirate", "SECBC_Pirate"),

                // Spacers from PawnKinds_Spacer.xml
                new BiocodeData("SpaceRefugee", "SECBC_SpaceRefugee"),
                new BiocodeData("SpaceRefugee_Clothed", "SECBC_SpaceRefugee_Clothed"),
                new BiocodeData("AncientSoldier", "SECBC_AncientSoldier"),

                // Empire from PawnKinds_Empire.xml
                new BiocodeData("Empire_Fighter_Trooper", "SECBC_Empire_Fighter_Trooper"),
                new BiocodeData("Empire_Fighter_Janissary", "SECBC_Empire_Fighter_Janissary"),
                new BiocodeData("Empire_Fighter_Champion", "SECBC_Empire_Fighter_Champion"),
                new BiocodeData("Empire_Fighter_Cataphract", "SECBC_Empire_Fighter_Cataphract"),
                new BiocodeData("Empire_Fighter_StellicGuardRanged", "SECBC_Empire_Fighter_StellicGuardRanged"),
                new BiocodeData("Empire_Fighter_StellicGuardMelee", "SECBC_Empire_Fighter_StellicGuardMelee"),
                new BiocodeData("Empire_Royal_NobleWimp", "SECBC_Empire_Royal_NobleWimp"),
                new BiocodeData("Empire_Royal_Yeoman", "SECBC_Empire_Royal_Yeoman"),
                new BiocodeData("Empire_Royal_Esquire", "SECBC_Empire_Royal_Esquire"),
                new BiocodeData("Empire_Royal_Knight", "SECBC_Empire_Royal_Knight"),
                new BiocodeData("Empire_Royal_Praetor", "SECBC_Empire_Royal_Praetor"),
                new BiocodeData("Empire_Royal_Baron", "SECBC_Empire_Royal_Baron"),
                new BiocodeData("Empire_Royal_Count", "SECBC_Empire_Royal_Count"),
                new BiocodeData("Empire_Royal_Duke", "SECBC_Empire_Royal_Duke"),
                new BiocodeData("Empire_Royal_Consul", "SECBC_Empire_Royal_Consul"),
                new BiocodeData("Empire_Royal_Stellarch", "SECBC_Empire_Royal_Stellarch")
            };

            BCD_Other = new BiocodeData(null, "SECBC_Other");
        }

        public void SetAllValues(float newBiocodeChance)
        {
            BiocodeData.ForEach(d => d.BiocodeChance = newBiocodeChance);
            BCD_Other.BiocodeChance = newBiocodeChance;
        }
    }
}
