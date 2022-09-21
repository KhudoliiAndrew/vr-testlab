using UnityEngine;

namespace wands.grab
{
    public class GrabWand : Wand
    {
        [Header("Wand parts")]
        public GameObject head;
        public GameObject beam;
        
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