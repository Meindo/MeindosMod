using HarmonyLib;
using UnityEngine;

namespace MeindosMod.patches;

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
class HudManagerUpdatePatch
{
    static void setPlayerNameColor(PlayerControl p, Color color) {
        p.cosmetics.nameText.color = color;
        if (MeetingHud.Instance != null)
            foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                if (player.NameText != null && p.PlayerId == player.TargetPlayerId)
                    player.NameText.color = color;
    }
    
    static void Postfix(HudManager __instance)
    {
        if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;
        var localPlayer = CachedPlayer.LocalPlayer.PlayerControl;
        setPlayerNameColor(localPlayer, RainbowUtils.RainbowShadow);
    }
}

[HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    class KillButtonDoClickPatch {
        public static bool Prefix(KillButton __instance) {
            __instance.SetEnabled();
            __instance.SetCoolDown(0, 0);
            __instance.ToggleVisible(true);
            return true;
        }
    }
