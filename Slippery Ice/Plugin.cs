using BepInEx;
using System;
using System.Reflection;
using System.ComponentModel;
using Utilla;
using HarmonyLib;
using SlipperyIce.Behaviours;

namespace SlipperyIce
{
    [ModdedGamemode]
    [Description("HauntedModMenu")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public Harmony iceHarmony;
        public IceHandler iHandler;

        public void Awake()
        {
            if (!(Instance is null)) return;
            Instance = this;

            iceHarmony = new Harmony(PluginInfo.GUID);
            iceHarmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        internal void OnEnable()
        {
            iHandler?.Enable();
            Console.WriteLine("Enabled the ice");
        }

        internal void OnDisable()
        {
            iHandler?.Disable();
            Console.WriteLine("Disabled the ice");
        }

        [ModdedGamemodeJoin] public void JoinedModded()
        {
            iHandler?.JoinedModded();
            Console.WriteLine("Joined modded room");
        }

        [ModdedGamemodeLeave] public void LeftModded()
        {
            iHandler?.LeftModded();
            Console.WriteLine("Left modded room");
        }
    }
}
