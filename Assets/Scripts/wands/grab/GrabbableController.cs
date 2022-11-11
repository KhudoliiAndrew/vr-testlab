using System;
using UnityEngine;

namespace wands.grab
{
    public class GrabbableController : MonoBehaviour
    {
        public GameObject raycastPointer;
        public GameObject targetPointer;
        public RaycastHit RayOutput;

        [HideInInspector] public GameObject selectedObject;
        private GameObject _lastHittedObject;

        private float _targetObjectPointDistance;

        private void Awake()
        {
            GetComponent<GrabWand>().OnThumbstickAxisCallback = OnThumbstickAxisCallback;
        }

        void FixedUpdate()
        {
            var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

            if (Physics.Raycast(ray, out RayOutput, 1000))
            {
                OnObjectCollided(RayOutput.transform.gameObject);
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
            
            if(targetPointer.transform.localPosition.y != _targetObjectPointDistance)
                targetPointer.transform.localPosition = new Vector3(0, _targetObjectPointDistance, 0);

            if (selectedObject != null)
            {
                var movingVector = (targetPointer.transform.position - selectedObject.transform.position).normalized;
                Vector3 dir = movingVector * 8f;

                selectedObject.GetComponent<Rigidbody>().velocity = dir;
            }
        }

        // GameObject here is always != null
        private void OnObjectCollided(GameObject hittedObject)
        {
            if (!IsGrabbing() && selectedObject != null) selectedObject = null;

            if (selectedObject != null) return;


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
            SetTargetObjectPointDistance(Vector3.Distance(o.transform.position, gameObject.transform.position));

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

        private void SetTargetObjectPointDistance(float position)
        {
            _targetObjectPointDistance = Math.Clamp(position, 2f, 10000);
        }
        
        private void OnThumbstickAxisCallback(Vector2 input)
        {
            SetTargetObjectPointDistance(_targetObjectPointDistance + input.y * .5f);
        }

        private bool IsGrabbing() => GetComponent<GrabWand>().isActive;
    }
}