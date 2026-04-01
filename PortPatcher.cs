using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace Deep_Ports
{
    [BepInPlugin("com.winter.deepports","Deep Ports", "0.3")]
    public class PortPatcher : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.winter.deepports");
        }

        public const string pluginGuid = "com.winter.deepports";
        public const string pluginName = "Deep Ports";
        public const string pluginVersion = "0.3";
    }
}
