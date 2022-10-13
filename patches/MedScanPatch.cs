using System;
using HarmonyLib;

namespace MeindosMod.patches;

public class MedScanPatch
{
    public static (String id, Int32 bloodType) PlayerData;

    [HarmonyPatch(typeof(MedScanMinigame))]
    private static class MedScanMinigamePatch
    {
        [HarmonyPatch(nameof(MedScanMinigame.Begin))]
        [HarmonyPostfix]
        private static void BeginPostfix(MedScanMinigame __instance)
        {
            if (PlayerData == default)
            {
                for (Int32 i = 0; i < 6; i++)
                {
                    Int32 id = new Random().Next(0, Int32.MaxValue);
                    PlayerData.id += id.ToString("X").PadLeft(8, '0');
                }

                PlayerData.bloodType = new Random().Next(0, 8);
            }

            __instance.completeString =
                "\nPlayer Name: " + PlayerControl.LocalPlayer.name +
                "\nHeight: 3 feet, 6 inches" +
                "\nWeight: 92 pounds" +
                "\nBlood Type: " + MedScanMinigame.BloodTypes[PlayerData.bloodType] +
                "\nMod made by: " + "<color=#BEA4FFFF>Meindo</color>";
            __instance.ScanDuration = 13f;
        }
    }

    [HarmonyPatch(typeof(ShipStatus))]
    private static class ShipStatusPatch
    {
        [HarmonyPatch(nameof(ShipStatus.Start))]
        [HarmonyPrefix]
        private static void StartPrefix()
        {
            PlayerData = default;
        }
    }
}