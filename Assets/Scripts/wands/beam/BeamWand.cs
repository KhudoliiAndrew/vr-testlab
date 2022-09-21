using UnityEngine;

namespace wands.beam
{
    public class BeamWand : Wand
    {
        public GameObject beam;
        public GameObject head;
        protected override void OnWandHolding(bool isHolding)
        {
            ChangeWandStatus(isHolding);
            base.OnWandHolding(isHolding);
        }

        private void ChangeWandStatus(bool isHolding)
        {
            beam.SetActive(isHolding);
            head.SetActive(isHolding);
        }
    }
}
