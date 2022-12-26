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

            try
            {
                var pitObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.StartsWith("pit"));
                foreach (var pitObj in pitObjects)
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

                Plugin.Instance.iHandler = iceHandler;
                if (Plugin.Instance.enabled) IceHandler.instance.Enable();
            }
            catch(Exception e)
            {
                if (IceObject is null) Console.WriteLine(string.Join(" ", "Failed to create ice:", "IceObject is null"));
                else if (IceHandlerObject is null) Console.WriteLine(string.Join(" ", "Failed to create ice:", "IceHandlerObject is null"));
                else Console.WriteLine(string.Join(" ", "Failed to create ice:", e));
            }
        }
    }
}
