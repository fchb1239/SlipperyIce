using UnityEngine;

namespace SlipperyIce.Behaviours
{
    public class Ice : MonoBehaviour
    {
        public bool isSlipping;

        private GorillaSurfaceOverride surfaceOverride;
        private int defaultOverride;

        private Collider collider;
        internal PhysicMaterial defaultMat;
        internal PhysicMaterial slipperyMat;

        internal void Start()
        {
            TryGetComponent(out surfaceOverride);
            TryGetComponent(out collider);
            defaultOverride = surfaceOverride is null ? 0 : surfaceOverride.overrideIndex;
        }

        internal void FixedUpdate()
        {
            if (!(collider is null)) collider.material = (Plugin.Instance.iHandler.isModded && Plugin.Instance.enabled) ? slipperyMat : defaultMat;
            if (!(surfaceOverride is null)) surfaceOverride.overrideIndex = (Plugin.Instance.iHandler.isModded && Plugin.Instance.enabled) ? 59 : defaultOverride;
        }
    }
}
