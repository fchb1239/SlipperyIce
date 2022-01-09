using System;
using UnityEngine;

namespace SlipperyIce.Behaviours
{
    public class IceHandler : MonoBehaviour
    {
        public static IceHandler instance;
        Material ice = Resources.Load<Material>("objects/character/materials/ice");
        Material ground = Resources.Load<Material>("objects/forest/materials/pitgroundwinter");
        public bool modEnabled = false;
        public bool isModded = false;

        void Awake()
        {
            instance = this;
        }

        public void Enable()
        {
            modEnabled = true;
            if (isModded)
                SetGroundTexture(ice);
        }

        public void Disable()
        {
            modEnabled = false;
            SetGroundTexture(ground);
        }

        public void JoinedModded()
        {
            isModded = true;
            if(modEnabled)
                SetGroundTexture(ice);
        }

        public void LeftModded()
        {
            isModded = false;
            SetGroundTexture(ground);
        }

        void SetGroundTexture(Material mat)
        {
            Material[] materials = transform.gameObject.GetComponent<MeshRenderer>().materials;
            materials[0] = mat;
            transform.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }
    }
}
