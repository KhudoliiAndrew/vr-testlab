using UnityEngine;

namespace wands.grab
{
    public class GrabWand : Wand
    {
        [Header("Wand parts")]
        public GameObject head;
        public GameObject beam;

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
            head.SetActive(isHolding);
        }
    }
}