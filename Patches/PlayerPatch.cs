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
            //this guy really changed Level/Forest to Level/forest lmao
            if (IceHandler.instance == null)
                GameObject.Find("Level/forest").AddComponent<IceHandler>();
            else
                Console.WriteLine("What the hell, how did this happen?");
            GameObject.Find("Level/forest").AddComponent<Ice>();

            /*
            GameObject.Find("Level/treeroom/tree/Tree").AddComponent<AxisInverter>();
            foreach (Transform child in GameObject.Find("Level/Forest/SmallTrees").transform)
                GameObject.Find(string.Format("Level/Forest/SmallTrees/{0}/SmallTree", child.gameObject.name)).AddComponent<AxisInverter>();
            */

            if (Plugin.modEnabledTemp)
                IceHandler.instance.Enable();

            Plugin.modLoaded = true;
        }
    }
}
