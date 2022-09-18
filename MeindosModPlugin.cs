using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace MeindosMod;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class MeindosModPlugin : BasePlugin
{
    public static string ModVersion = "1.0.7";
    public Harmony Harmony { get; } = new(Id);

    public ConfigEntry<bool> Config0 { get; private set; }

    public override void Load()
    {
        Config0 = Config.Bind("MeindosMod","Light Cheat", false);
        Harmony.PatchAll();
        FileWriter.CreateTFile();
    }
}
