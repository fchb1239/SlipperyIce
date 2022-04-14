using System;
using UnityEngine;

namespace SlipperyIce.Behaviours
{
    class Ice : MonoBehaviour
    {
        public static Ice instance;
        public bool isSlipping;
        GorillaLocomotion.Player player = GorillaLocomotion.Player.Instance;
        //MeshCollider meshCol;
        //PhysicMaterial physicMat = Resources.Load<PhysicMaterial>("objects/forest/materials/Slippery");
        Rigidbody rb;
        Vector3 vel;

        void Awake()
        {
            instance = this;
            //the physicmat makes it slide better along the ground... i think
            //meshCol = transform.GetComponent<MeshCollider>();
            rb = player.GetComponent<Rigidbody>();
        }

        public void Invert(bool isX)
        {
            if (isX)
                vel.x = vel.x - (vel.x * 2);
            else
                vel.z = vel.z - (vel.z * 2);
        }

        void OnCollisionEnter(Collision col)
        {
            //if (IceHandler.instance.modEnabled && IceHandler.instance.isModded && player.transform.position.y <= 5.5f)
            if (IceHandler.instance.modEnabled && IceHandler.instance.isModded)
            {
                isSlipping = true;
                //Console.WriteLine("Starting to slip");
                //player.defaultSlideFactor = 1f;
                //HarmonyLib.Traverse.Create(player).Field("slipPercentage").SetValue(1f);
                //meshCol.material = physicMat;
                vel = rb.velocity;
                vel.y = 0;
            }
        }

        void OnCollisionExit(Collision col)
        {
            isSlipping = false;
            //Console.WriteLine("No longer slipping");
            //player.defaultSlideFactor = 0.03f;
            //meshCol.material = new PhysicMaterial();
        }

        void Update()
        {
            if (isSlipping)
                rb.velocity = vel;
        }
    }
}
