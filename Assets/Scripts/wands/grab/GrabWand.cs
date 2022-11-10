using UnityEngine;

namespace wands.grab
{
    public class GrabWand : Wand
    {
        [Header("Wand parts")]
        public GameObject head;
        public GameObject beam;

        private BeamController _beamController;

        protected override void Awake()
        {
            _beamController = gameObject.GetComponent<BeamController>();
        }

        protected override void OnWandHolding(bool isHolding)
        {
            ChangeWandStatus(isHolding);
            base.OnWandHolding(isHolding);
        }

        private void ChangeWandStatus(bool isHolding)
        {
            _beamController.SetBeamVisibility(isHolding);
            head.SetActive(isHolding);
        }
    }
}