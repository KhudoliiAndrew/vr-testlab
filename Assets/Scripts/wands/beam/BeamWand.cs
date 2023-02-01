using UnityEngine;

namespace wands.beam
{
    public class BeamWand : Wand
    {
        public GameObject beam;
        public GameObject head;
        
        public bool isActive;

        protected override void OnWandHolding(bool isHolding)
        {
            isActive = isHolding;

            ChangeWandStatus(isHolding);
            base.OnWandHolding(isHolding);
        }

        private void ChangeWandStatus(bool isHolding)
        {
            isActive = isHolding;

            beam.SetActive(isHolding);
            //head.SetActive(isHolding);
        }
    }
}
