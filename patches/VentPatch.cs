using HarmonyLib;
using MeindosMod.Utils;
using UnityEngine;

namespace MeindosMod.patches;


[HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
public static class VentPatch {
    public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)] GameData.PlayerInfo pc,
        [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse)
    {
        float num = float.MaxValue;
        PlayerControl @object = pc.Object;
        bool r = true;

        if (__instance.name.StartsWith("SealedVent_"))
        {
            canUse = couldUse = r;
            __result = num;
            return false;
        }
        couldUse = r;
        canUse = couldUse;
        var usableDistance = __instance.UsableDistance;
        if (canUse)
        {
            Vector3 center = @object.Collider.bounds.center;
            Vector3 position = __instance.transform.position;
            num = Vector2.Distance(center, position);
            canUse &= (num <= usableDistance &&
                       !PhysicsHelpers.AnythingBetween(@object.Collider, center, position, Constants.ShipOnlyMask,
                           false));
        }
        __result = num;
        return false;
    }
}

[HarmonyPatch(typeof(Vent), nameof(Vent.Use))]
    public static class VentUsePatch {
        public static bool Prefix(Vent __instance) {
            __instance.SetButtons(true);
            return false;
        }
    }

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
class VentButtonVisibilityPatch
{
    static void Postfix(PlayerControl __instance)
    {
        if (__instance.AmOwner && FastDestroyableSingleton<HudManager>.Instance.ReportButton.isActiveAndEnabled) {
            FastDestroyableSingleton<HudManager>.Instance.ImpostorVentButton.Show();
        }
    }
}
[HarmonyPatch(typeof(VentButton), nameof(VentButton.SetTarget))]
class VentButtonSetTargetPatch {
    static Sprite defaultVentSprite = null;
    static void Postfix(VentButton __instance) {
        defaultVentSprite = __instance.graphic.sprite;
        bool isSpecialVent = true;
        __instance.graphic.sprite = defaultVentSprite;
        __instance.buttonLabelText.enabled = isSpecialVent;
    }
}