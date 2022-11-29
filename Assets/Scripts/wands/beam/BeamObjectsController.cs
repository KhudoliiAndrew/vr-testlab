using UnityEngine;

namespace wands.beam
{
    public class BeamObjectsController : MonoBehaviour
    {
        public GameObject raycastPointer;
        public GameObject Bullet_Mark;

        private RaycastHit _rayOutput;

        public float targetObjectPointDistance;

        private const float MaxSpeed = 7f;

        void Update()
        {
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            if (Physics.Raycast(ray, out _rayOutput, 1000))
            {
                targetObjectPointDistance = Vector3.Distance(_rayOutput.point, gameObject.transform.position);

                if (!IsEnabled()) return;
                
                GameObject hittedObject = _rayOutput.transform.gameObject;
                Rigidbody objectRigidbody = hittedObject.GetComponent<Rigidbody>();
                
                /// 

                var hitMark = Instantiate(Bullet_Mark, _rayOutput.point, Quaternion.LookRotation(_rayOutput.normal));
                hitMark.transform.Rotate(Vector3.up * 180);
                hitMark.transform.Translate(Vector3.back * .01f);
 
                hitMark.transform.SetParent(hittedObject.transform);
                Destroy(hitMark, 1.5f);
                
                ///
                
                if (objectRigidbody == null) return;

                if(objectRigidbody.velocity.magnitude > MaxSpeed)
                {
                    objectRigidbody.velocity = objectRigidbody.velocity.normalized * MaxSpeed;
                }
                
                objectRigidbody.AddForce((_rayOutput.point - gameObject.transform.position).normalized * .4f,
                    ForceMode.Impulse);
            }
        }

        private bool IsEnabled() => GetComponent<BeamWand>().isActive;
    }
}