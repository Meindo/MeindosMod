using HarmonyLib;
using Reactor.Utilities;
using UnityEngine;

namespace MeindosMod.patches
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
    public class Lights
    {
        public static bool Prefix(ShipStatus __instance, [HarmonyArgument(0)] GameData.PlayerInfo player,
            ref float __result)
        {
            if (PluginSingleton<MeindosModPlugin>.Instance.Lights.Value)
            {
                __result = 255f;
                return false;
            }
            if (player == null || player.IsDead)
            {
                __result = __instance.MaxLightRadius;
                return false;
            }
            if (player.Role.IsImpostor)
            {
                __result =
                    __instance.MaxLightRadius * GameOptionsManager.Instance.currentNormalGameOptions.ImpostorLightMod;
                return false;
            }
            var switchSystem = __instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
            var t = switchSystem.Value / 255f;
            __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, t) *
                       GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod;
            return false;
        }
    }
}