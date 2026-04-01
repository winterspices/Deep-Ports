using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Deep_Ports
{
    internal class Patches
    {
        [HarmonyPatch(typeof(FloatingOriginManager))]
        public static class FloatingOriginManagerPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch("Start")]
            public static void StartPatch(FloatingOriginManager __instance)
            {
                if(__instance.name == "_shifting world")
                {
                    TerrainSetup();

                    if(data.Count > 0)
                    {
                        Transform fa = GameObject.Find("island 15 M (Fort)").transform.Find("Terrain");
                        fa.GetComponent<Terrain>().terrainData = data["fa"];
                        fa.GetComponent<TerrainCollider>().terrainData = data["fa"];

                        Transform grc = GameObject.Find("island 1 A (gold rock)").transform.Find("Terrain");
                        grc.GetComponent<Terrain>().terrainData = data["grc"];
                        grc.GetComponent<TerrainCollider>().terrainData = data["grc"];

                        Debug.LogWarning("Custom ports loaded");
                    }
                }
            }
        }

        private static void TerrainSetup()
        {
            string path = Paths.PluginPath + "\\Deep Ports\\deepports";
            if (!File.Exists(path))
            {
                Debug.LogError("Deep Ports not installed correctly");
                terrainInstalled = false;
            }
            else
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(path);

                data["fa"] = bundle.LoadAsset<TerrainData>("fortaestrin");
                data["grc"] = bundle.LoadAsset<TerrainData>("grc");

                terrainInstalled = true;
                Debug.LogWarning("Port assets loaded");
            }
        }

        public static Dictionary<string,TerrainData> data = new Dictionary<string, TerrainData>();
        public static bool terrainInstalled;
    }
}
