using UnityEngine;

namespace wands.grab
{
    public class GrabbableController : MonoBehaviour
    {
        public GameObject raycastPointer;
        public GameObject targetPointer;
        public RaycastHit RayOutput;

        public GameObject selectedObject;
        private GameObject _lastHittedObject;

        void FixedUpdate()
        {
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            if (Physics.Raycast(ray, out RayOutput, 1000))
            {
                OnObjectCollided(RayOutput);
            }
            else
            {
                if (!IsGrabbing())
                {
                    ReleaseLastObject();
                    _lastHittedObject = null;
                    selectedObject = null;
                }
            }

            if (selectedObject != null)
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position,
                    targetPointer.transform.position, Time.deltaTime * 1.5f);
            }
        }

        private void OnObjectCollided(RaycastHit hit)
        {
            if (!IsGrabbing() && selectedObject != null) selectedObject = null;
            
            if (selectedObject != null) return;

            // GameObject here is always != null
            GameObject hittedObject = hit.transform.gameObject;

            // The wand is pointed to the new object
            ReleaseLastObject();

            if (hittedObject.GetComponent<Rigidbody>() == null)
            {
                _lastHittedObject = null;
                return;
            }
            
            ControlSelect(hittedObject, SelectableType.Hover);


            if (IsGrabbing()) OnWandReadyToGrab(hittedObject);
            else UpdateObjectPhysicsParameters(hittedObject, true);

            _lastHittedObject = hittedObject;
        }

        private void OnWandReadyToGrab(GameObject o)
        {
            targetPointer.transform.localPosition = new Vector3(0,
                Vector3.Distance(o.transform.position, gameObject.transform.position), 0);
            
            selectedObject = o;

            UpdateObjectPhysicsParameters(selectedObject, false);
            ControlSelect(selectedObject, SelectableType.Selected);
        }

        private void ReleaseLastObject()
        {
            UpdateObjectPhysicsParameters(_lastHittedObject, true);
            ControlSelect(_lastHittedObject, SelectableType.Unselected);
        }

        private void UpdateObjectPhysicsParameters(GameObject gameObject, bool isPhysics)
        {
            if (gameObject == null || gameObject.GetComponent<Rigidbody>() == null) return;

            gameObject.GetComponent<Rigidbody>().useGravity = isPhysics;
        }

        private void ControlSelect(GameObject gameObject, SelectableType type)
        {
            if (gameObject == null || gameObject.GetComponent<Rigidbody>() == null) return;

            if (gameObject.GetComponent<Selectable>() == null)
                gameObject.AddComponent<Selectable>();

            gameObject.GetComponent<Selectable>().UpdateStatus(type);
        }

        private bool IsGrabbing() => GetComponent<GrabWand>().beam.activeSelf;
    }
}