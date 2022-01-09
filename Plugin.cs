using System;
using System.Reflection;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using SlipperyIce.Behaviours;
using Utilla;

namespace SlipperyIce
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        public static bool modLoaded;
        public static bool modEnabledTemp = false;

        public void Awake()
        {
            new Harmony(PluginInfo.GUID).PatchAll(Assembly.GetExecutingAssembly());
        }

        void OnEnable()
        {
            if (modLoaded)
                IceHandler.instance.Enable();
            else
                modEnabledTemp = true;

            Console.WriteLine("Enabled the ice");
        }

        void OnDisable()
        {
            if (modLoaded)
                IceHandler.instance.Disable();
            else
                modEnabledTemp = false;

            Console.WriteLine("Disabled the ice");
        }

        [ModdedGamemodeJoin]
        void JoinedModded()
        {
            IceHandler.instance.JoinedModded();
            Console.WriteLine("Joined modded room");
        }

        [ModdedGamemodeLeave]
        void LeftModded()
        {
            IceHandler.instance.LeftModded();
            Console.WriteLine("Left modded room");
        }
    }
}
