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
            SquirtingElephantSettings.ApplySettings();
        }
    }
}
