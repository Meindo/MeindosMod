﻿using HarmonyLib;


namespace MeindosMod.patches
{
    public class SpeedPatch
    {
        [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
        [HarmonyPostfix]
        public static void PostfixPhysics(PlayerPhysics __instance)
        {
            //if (__instance.AmOwner && GameData.Instance && __instance.myPlayer.CanMove && !__instance.myPlayer.Data.IsDead) 
                //__instance.body.velocity *= 1f;
        }

        [HarmonyPatch(typeof(CustomNetworkTransform), nameof(CustomNetworkTransform.FixedUpdate))]
        [HarmonyPostfix]
        public static void PostfixNetwork(CustomNetworkTransform __instance)
        {
            if (!__instance.AmOwner && __instance.interpolateMovement != 0.0f && !__instance.gameObject.GetComponent<PlayerControl>().Data.IsDead)
            {
                var player = __instance.gameObject.GetComponent<PlayerControl>();
               // __instance.body.velocity *= 1f;
            }
        }
    }
}
