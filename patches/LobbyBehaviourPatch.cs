using HarmonyLib;
using UnityEngine;

namespace MeindosMod.patches
{
    [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
    public class LobbyBehaviourPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            (DestroyableSingleton<HudManager>.Instance.FullScreen).gameObject.active = false;
        }
    }
}
