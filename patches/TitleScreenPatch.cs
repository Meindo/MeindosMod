using HarmonyLib;

namespace MeindosMod.patches
{
    public class TitleScreenPatch
    {
        [HarmonyPriority(Priority.VeryHigh)]
        [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
        public static class TScreenPatch
        {
            public static void Postfix(VersionShower __instance)
            {
                var text = __instance.text;
                text.text += $" - <color=#00FF00FF>MeindosMod {MeindosModPlugin.ModVersion}</color>";
            }
        }
    }
}
