using UnityEngine;

namespace wands.beam
{
    public class BeamObjectsController : MonoBehaviour
    {
        public GameObject raycastPointer;
        public RaycastHit RayOutput;

        public float TargetObjectPointDistance;

        void FixedUpdate()
        {
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            if (Physics.Raycast(ray, out RayOutput, 1000))
            {
                TargetObjectPointDistance = Vector3.Distance(RayOutput.point, gameObject.transform.position);
            }
            else
            {
                if (!IsEnabled())
                {
                }
            }
        }

        private bool IsEnabled() => GetComponent<BeamWand>().isActive;
    }
}