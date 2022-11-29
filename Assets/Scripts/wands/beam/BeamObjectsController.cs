using UnityEngine;

namespace wands.beam
{
    public class BeamObjectsController : MonoBehaviour
    {
        public GameObject raycastPointer;
        private RaycastHit _rayOutput;

        public float targetObjectPointDistance;

        private const float MaxSpeed = 7f;

        void FixedUpdate()
        {
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            if (Physics.Raycast(ray, out _rayOutput, 1000))
            {
                targetObjectPointDistance = Vector3.Distance(_rayOutput.point, gameObject.transform.position);

                if (!IsEnabled()) return;
                
                GameObject hittedObject = _rayOutput.transform.gameObject;
                Rigidbody objectRigidbody = hittedObject.GetComponent<Rigidbody>();
                if (objectRigidbody == null) return;

                if(objectRigidbody.velocity.magnitude > MaxSpeed)
                {
                    objectRigidbody.velocity = objectRigidbody.velocity.normalized * MaxSpeed;
                }
                
                objectRigidbody.AddForce((_rayOutput.point - gameObject.transform.position).normalized * .2f,
                    ForceMode.Impulse);
            }
        }

        private bool IsEnabled() => GetComponent<BeamWand>().isActive;
    }
}