﻿using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace MeindosMod.patches
{
    public class SteamPatch
    {
        [HarmonyPatch]
                public static class RestartAppIfNecessaryPatch
                {
                    private const string TypeName = "Steamworks.SteamAPI, Assembly-CSharp-firstpass";
                    private const string MethodName = "RestartAppIfNecessary";
        
                    public static bool Prepare()
                    {
                        return Type.GetType(TypeName, false) != null;
                    }
                    public static MethodBase TargetMethod()
                    {
                        return AccessTools.Method(TypeName + ":" + MethodName);
                    }
                    public static bool Prefix(out bool __result)
                    {
                        const string file = "steam_appid.txt";
        
                        if (!File.Exists(file)) File.WriteAllText(file, "945360");
        
                        return __result = false;
                    }
            }
    }
    
}
