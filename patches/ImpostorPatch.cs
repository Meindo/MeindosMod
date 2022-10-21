using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MeindosMod.patches;

[HarmonyPatch(typeof (PlayerControl), nameof(PlayerControl.SetRole))]
public static class SeeImpostor
{
    public static void Postfix(PlayerControl __instance)
    {
        foreach (GameData.PlayerInfo allPlayer in GameData.Instance.AllPlayers)
        {
            if (allPlayer.Role.IsImpostor)
                ((TMP_Text) allPlayer.Object.name).color = new Color(1f, 0.098f, 0.098f, 1f);
        }
    }
}

[HarmonyPatch(typeof (PlayerControl), "FixedUpdate")]
public static class PlayerOffensive
{
    public static void Postfix(PlayerControl __instance)
    {
        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            PlayerControl.LocalPlayer.SetKillTimer(0);
        if (PlayerControl.LocalPlayer.inVent == PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            PlayerControl.LocalPlayer.moveable = true;
        if (Input.GetKeyDown(KeyCode.F1))
            MeetingHud.Instance.CmdCastVote(__instance.PlayerId,  0);
        if (!Input.GetKeyDown(KeyCode.F2))
            return;
        typeof(PlayerControl).GetProperty(nameof(PlayerControl.LocalPlayer.Data.Role.IsImpostor))?.SetValue(PlayerControl.LocalPlayer.Data.Role.IsImpostor, true);
    }
}

[HarmonyPatch(typeof (MapBehaviour), "FixedUpdate")]
internal class CrewMapImpostor
{
    private static void Postfix(MapBehaviour __instance)
    {
        __instance.infectedOverlay.Start();
        ((Behaviour) __instance.infectedOverlay).enabled = true;
        ((Component) __instance.infectedOverlay).gameObject.SetActive(true);
    }
}