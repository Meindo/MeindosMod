using System;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MeindosMod.patches;
using Reactor;
using Reactor.Utilities;

namespace MeindosMod;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class MeindosModPlugin : BasePlugin
{
    public static string ModVersion = "1.3.0";
    public Harmony Harmony { get; } = new(Id);
    public ConfigEntry<bool> Lights { get; private set; }
    public static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("MeindosMod");
    public ConfigEntry<bool> Vents { get; private set; }
    public override void Load()
    {
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
    public static IRegionInfo[] MergeRegions(
        IRegionInfo[] oldRegions,
        IRegionInfo[] newRegions)
    {
        IRegionInfo[] destinationArray = new IRegionInfo[oldRegions.Length + newRegions.Length];
        Array.Copy(oldRegions, destinationArray, oldRegions.Length);
        Array.Copy(newRegions, 0, destinationArray, oldRegions.Length, newRegions.Length);
        return destinationArray;
    }

    public static IRegionInfo AddRegion(string name, string ip, bool useDtls)
    {
        if (Uri.CheckHostName(ip) != UriHostNameType.IPv4)
            return DestroyableSingleton<ServerManager>.Instance.CurrentRegion;
        IRegionInfo iregionInfo1 = ServerManager.DefaultRegions.ToArray().FirstOrDefault((Func<IRegionInfo, bool>) (region => region.PingServer == ip));
        if (iregionInfo1 != null)
            return iregionInfo1;
        IRegionInfo iregionInfo2 = new DnsRegionInfo(ip, name, (StringNames) 1003, ip, 22023, useDtls).Cast<IRegionInfo>();
        RegionPatch.ModRegions.Add(iregionInfo2);
        RegionPatch.Patch();
        return iregionInfo2;
    }
}