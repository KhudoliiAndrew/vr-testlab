using UnityEngine;

namespace wands.grab
{
    public class GrabbableController : MonoBehaviour
    {
        [Header("Beam Parameters")] public Transform startBeam;
        public Transform beam2;
        public Transform beam3;
        public Transform endBeam;

        public Transform startTarget;

        private Vector3 _collision = Vector3.zero;
        private GameObject lastHittedObject;

        public GameObject raycastPointer;

        void FixedUpdate()
        {
            if (!ShouldBeUpdated()) return;

            startBeam.position = startTarget.position;

            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                var o = hit.transform.gameObject;

                if (o != lastHittedObject)
                {
                    UpdateObjectPhysicsParameters(lastHittedObject,true);
                }

                if (o != null && o.GetComponent<Rigidbody>() == null) return;
                o.GetComponent<Rigidbody>().useGravity = false;

                endBeam.position = hit.point;
                var position = raycastPointer.transform.position;
                beam2.position = (position + hit.point) * 0.3f;
                beam3.position = (position + hit.point) * 0.6f;
                _collision = hit.point;

                hit.transform.gameObject.transform.position = Vector3.Lerp(o.transform.position,
                    hit.point, Time.deltaTime * 1.5f);


                UpdateObjectPhysicsParameters(o,false);
                lastHittedObject = o;
            }
            else
            {
                UpdateObjectPhysicsParameters(lastHittedObject,true);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_collision, 1.0f);
        }

        private void UpdateObjectPhysicsParameters(GameObject gameObject, bool isPhysics)
        {
            if (gameObject != null)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = isPhysics;
            }
        }

        private bool ShouldBeUpdated()
        {
            return this.GetComponent<GrabWand>().beam.activeSelf;
        }
    }
}