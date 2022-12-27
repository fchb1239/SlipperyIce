using System;
using HarmonyLib;
using UnityEngine;
using SlipperyIce.Behaviours;
using System.Linq;

namespace SlipperyIce.Patches
{
    // Ensures the scene is loaded
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "Awake")]
    internal class PlayerPatch
    {
        public static void Postfix()
        {
            GameObject IceObject = null;
            GameObject IceHandlerObject = null;
            PhysicMaterial IceMaterial = null;

            try
            {
                foreach (var pitObj in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.StartsWith("pit")))
                {
                    if (pitObj.name.EndsWith("ground"))
                    {
                        IceObject = pitObj;
                        IceHandlerObject = pitObj;
                        break;
                    }
                }


                IceHandler iceHandler = IceHandlerObject.AddComponent<IceHandler>();
                Ice ice = IceObject.AddComponent<Ice>();
                ice.defaultMat = IceObject.GetComponent<Collider>().material;

                var iceObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "mountainsideice");
                if (iceObjects.ToList().Count == 1) ice.slipperyMat = iceObjects.ToList()[0].GetComponent<Collider>().material;

                Plugin.Instance.iHandler = iceHandler;
            }
            catch(Exception e)
            {
                if (IceObject is null) Console.WriteLine(string.Join(" ", "Failed to create ice:", "IceObject is null"));
                else if (IceHandlerObject is null) Console.WriteLine(string.Join(" ", "Failed to create ice:", "IceHandlerObject is null"));
                else if (IceMaterial is null) Console.WriteLine(string.Join(" ", "Failed to create ice:", "IceMaterial is null"));
                else Console.WriteLine(string.Join(" ", "Failed to create ice:", e));
            }
        }
    }
}
