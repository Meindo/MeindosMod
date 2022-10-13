using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using MeindosMod.patches;
using Reactor;

namespace MeindosMod;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class MeindosModPlugin : BasePlugin
{
    public static string ModVersion = "1.2.0";
    public Harmony Harmony { get; } = new(Id);
    public ConfigEntry<bool> Lights { get; private set; }
    public ConfigEntry<bool> Vents { get; private set; }
    public override void Load()
    {
        var logger = BepInEx.Logging.Logger.CreateLogSource("MeindosMod");
        logger.LogInfo("Loading MeindosMod...");
        Lights = Config.Bind("MeindosMod","Light Cheat", false, "Always have impostor vision");
        Vents = Config.Bind("MeindosMod","Engineer", false, "Allows you to always vent, no matter the role");
        Harmony.PatchAll();
        if (!PluginSingleton<MeindosModPlugin>.Instance.Vents.Value)
        {
            Harmony.Unpatch(typeof(VentPatch).GetMethod("Prefix"), HarmonyPatchType.Prefix);
            Harmony.Unpatch(typeof(VentUsePatch).GetMethod("Prefix"), HarmonyPatchType.Prefix);
            Harmony.Unpatch(typeof(VentButtonVisibilityPatch).GetMethod("Postfix"), HarmonyPatchType.Postfix);
            Harmony.Unpatch(typeof(VentButtonSetTargetPatch).GetMethod("Postfix"), HarmonyPatchType.Postfix);
            logger.LogMessage("Successfully unpatched vent patches");
        }
        FileWriter.CreateTFile();
        logger.LogMessage("Finished loading MeindosMod");
    }
}
