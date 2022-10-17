using Il2CppSystem.Collections.Generic;
using CollectionExtensions = HarmonyLib.CollectionExtensions;

namespace MeindosMod.patches;

public class RegionPatch
{
    private static readonly IRegionInfo[] OldRegions = ServerManager.DefaultRegions;

    private static IRegionInfo a =
        MeindosModPlugin.AddRegion("<color=\"orange\">skeld.net", "159.223.173.35", false);
        private static readonly IRegionInfo[] NewRegions = {
            a
        };
        public static List<IRegionInfo> ModRegions = new();
        public static IRegionInfo DirectRegion;
        public static void Patch()
        {
            ServerManager instance = DestroyableSingleton<ServerManager>.Instance;
            IRegionInfo[] newRegions = MeindosModPlugin.MergeRegions(NewRegions, ModRegions.ToArray());
            if (DirectRegion != null)
                newRegions = CollectionExtensions.AddToArray(newRegions, DirectRegion);
            IRegionInfo[] iregionInfoArray = MeindosModPlugin.MergeRegions(OldRegions, newRegions);
            ServerManager.DefaultRegions = iregionInfoArray;
            instance.AvailableRegions = iregionInfoArray;
            instance.SaveServers();
        }
}