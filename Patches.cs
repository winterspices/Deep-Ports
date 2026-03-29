using BepInEx;
using HarmonyLib;
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

                    Debug.LogWarning(terrain);

                    if(terrain)
                    {
                        Debug.LogWarning("Loading custom ports");

                        Transform fa = GameObject.Find("island 15 M (Fort)").transform.Find("Terrain");
                        fa.GetComponent<Terrain>().terrainData = terrain;
                        fa.GetComponent<TerrainCollider>().terrainData = terrain;

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
                string td = "fortaestrin";
                terrain = bundle.LoadAsset<TerrainData>(td);
                terrainInstalled = true;
                Debug.LogWarning("Port assets loaded");
            }
        }

        public static TerrainData terrain;

        public static bool terrainInstalled;
    }
}
