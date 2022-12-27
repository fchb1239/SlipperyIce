using HarmonyLib;
using UnityEngine;
using GorillaLocomotion;

namespace SlipperyIce.Behaviours
{
    public class Ice : MonoBehaviour
    {
        public bool isSlipping;

        internal Player gPlayer = Player.Instance;
        internal Rigidbody rBody;
        internal Vector3 rVelocity;

        internal GorillaSurfaceOverride surfaceOverride;
        internal int defaultOverride;

        internal void Start()
        {
            // Physics materials work it's just that they're too realistic for this silly monke game
            TryGetComponent(out surfaceOverride);
            defaultOverride = surfaceOverride is null ? 0 : surfaceOverride.overrideIndex;
            rBody = Traverse.Create(gPlayer).Field("playerRigidBody").GetValue() as Rigidbody;
        }

        internal void Invert(bool isX)
        {
            if (isX) rVelocity.x -= (rVelocity.x * 2);
            else rVelocity.z -= (rVelocity.z * 2);
        }

        internal void OnCollisionEnter(Collision collider)
        {
            if (!(collider.gameObject.name is "GorillaPlayer")) return;
            if (!(Plugin.Instance.iHandler.isModded && Plugin.Instance.enabled)) return;

            isSlipping = true;
            rVelocity = rBody.velocity;
            rVelocity.y = 0;
        }

        internal void OnCollisionExit(Collision collider)
        {
            if (!(collider.gameObject.name is "GorillaPlayer")) return;
            isSlipping = false;
        }

        internal void FixedUpdate()
        {
            if (isSlipping && !(rBody is null)) rBody.velocity = rVelocity;
            if (!(surfaceOverride is null)) surfaceOverride.overrideIndex = (Plugin.Instance.iHandler.isModded && Plugin.Instance.enabled) ? 59 : defaultOverride;
        }
    }
}
