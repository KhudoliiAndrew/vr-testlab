using System;
using UnityEngine;

namespace wands.beam
{
    public class BeamObjectsController : MonoBehaviour
    {
        public GameObject raycastPointer;
        public float targetObjectPointDistance;
        
        private RaycastHit _rayOutput;
        private const float MaxSpeed = 7f;

        private BeamHitmarkHelper _beamHitmarkHelper;

        private void Awake()
        {
            _beamHitmarkHelper = GetComponent<BeamHitmarkHelper>();
        }

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
                _beamHitmarkHelper.CreateHitmarks(_rayOutput.point, _rayOutput.normal, hittedObject.transform);
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