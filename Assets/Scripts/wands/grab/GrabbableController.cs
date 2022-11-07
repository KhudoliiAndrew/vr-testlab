using UnityEngine;

namespace wands.grab
{
    public class GrabbableController : MonoBehaviour
    {
        [Header("Beam Parameters")] public Transform startBeam;
        public Transform beam2;
        public Transform beam3;
        public Transform endBeam;
        public Transform pointerBeam;

        public Transform startTarget;

        private Vector3 _collision = Vector3.zero;
        private GameObject lastHittedObject;

        public GameObject raycastPointer;

        void FixedUpdate()
        {
            startBeam.position = startTarget.position;
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);
            RaycastHit hit;
           
            if (Physics.Raycast(ray, out hit, 1000))
            {
                var o = hit.transform.gameObject;

                if (o != null && o.GetComponent<Rigidbody>() != null)
                    ControlSelect(o, SelectableType.Hover);
                
                if (o != lastHittedObject)
                {
                    UpdateObjectPhysicsParameters(lastHittedObject, true);
                    ControlSelect(lastHittedObject, SelectableType.Unselected);
                    lastHittedObject = o;
                }

                UpdateHoverBeam(Vector3.Distance(hit.point, transform.position));
                
                if (!ShouldBeUpdated())
                {
                    UpdateObjectPhysicsParameters(o, true);
                    return;
                }

                if (o != null && o.GetComponent<Rigidbody>() == null) return;

                endBeam.position = hit.point;
                _collision = hit.point;

                hit.transform.gameObject.transform.position = Vector3.Lerp(o.transform.position,
                    hit.point, Time.deltaTime * 1.5f);

                UpdateObjectPhysicsParameters(o, false);
                ControlSelect(lastHittedObject, SelectableType.Selected);
            }
            else
            {
                UpdateObjectPhysicsParameters(lastHittedObject, true);
                ControlSelect(lastHittedObject, SelectableType.Unselected);

            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_collision, 1.0f);
        }

        private void UpdateObjectPhysicsParameters(GameObject gameObject, bool isPhysics)
        {
            if (gameObject == null || gameObject.GetComponent<Rigidbody>() == null) return;

            gameObject.GetComponent<Rigidbody>().useGravity = isPhysics;
        }

        private void ControlSelect(GameObject gameObject, SelectableType type)
        {
            if(gameObject == null || gameObject.GetComponent<Rigidbody>() == null) return;
            
            if (gameObject.GetComponent<Selectable>() == null)
                gameObject.AddComponent<Selectable>();

            gameObject.GetComponent<Selectable>().UpdateStatus(type);
        }

        private void UpdateHoverBeam(float distance)
        {
            pointerBeam.localScale = new Vector3(pointerBeam.localScale.x, distance, pointerBeam.localScale.z);
            pointerBeam.transform.localPosition = new Vector3(0, distance, 0);
        }

        private bool ShouldBeUpdated()
        {
            return GetComponent<GrabWand>().beam.activeSelf;
        }
    }
}