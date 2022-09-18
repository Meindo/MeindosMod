using HarmonyLib;
using UnityEngine;

namespace MeindosMod.patches
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public class PingTrackerUpdate
    {
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.1f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text =
                $"<color=#00FF00FF>MeindosMod {MeindosModPlugin.ModVersion}</color>\n" +
                "Made by <color=#BEA4FFFF>Meindo</color>\n"+
                $"GPU: {SystemInfo.GetGraphicsDeviceName()}\n"+
                $"Ping: {AmongUsClient.Instance.Ping}ms";
        }
    }
}
