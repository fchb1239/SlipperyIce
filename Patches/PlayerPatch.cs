using System;
using HarmonyLib;
using UnityEngine;
using SlipperyIce.Behaviours;

namespace SlipperyIce.Patches
{
    //i need to make sure the scene is loaded
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Awake")]
    class PlayerPatch
    {
        private static void Postfix()
        {
            if (IceHandler.instance == null)
                GameObject.Find("Level/Forest").AddComponent<IceHandler>();
            else
                Console.WriteLine("What the hell, how did this happen?");
            GameObject.Find("Level/Forest").AddComponent<Ice>();

            if (Plugin.modEnabledTemp)
                IceHandler.instance.Enable();

            Plugin.modLoaded = true;
        }
    }
}
