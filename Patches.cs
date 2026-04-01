using BepInEx;
using HarmonyLib;
using System;
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

                    if (data.Count > 0)
                    {
                        Transform fa = GameObject.Find("island 15 M (Fort)").transform.Find("Terrain");
                        fa.GetComponent<Terrain>().terrainData = data["fa"];
                        fa.GetComponent<TerrainCollider>().terrainData = data["fa"];

                        Transform grc = GameObject.Find("island 1 A (gold rock)").transform.Find("Terrain");
                        grc.GetComponent<Terrain>().terrainData = data["grc"];
                        grc.GetComponent<TerrainCollider>().terrainData = data["grc"];

                        Transform dc = GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Terrain");
                        dc.GetComponent<Terrain>().terrainData = data["dc"];
                        dc.GetComponent<TerrainCollider>().terrainData = data["dc"];
                        GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Cube_003").transform.localPosition = new Vector3(525f, -14f, -218f);
                        GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Cube_005").transform.localPosition = new Vector3(525f, 3f, -233f);

                        Debug.LogWarning("Custom ports loaded");
                    }
                }
            }
        }

        private static void TerrainSetup()
        {
            try
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
                    data["dc"] = bundle.LoadAsset<TerrainData>("dc");

                    terrainInstalled = true;
                    Debug.LogWarning("Port assets loaded");
                }
            } catch (Exception e)
            {
                Debug.LogError("Uh oh! Something went wrong with loading deep port assets.");
            }
            
        }

        public static Dictionary<string,TerrainData> data = new Dictionary<string, TerrainData>();
        public static bool terrainInstalled;
    }
}
