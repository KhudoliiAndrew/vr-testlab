using UnityEngine;
using UnityEngine.Serialization;

namespace wands.lighter
{
    public class LighterWand : Wand
    {
        public GameObject spotLight;
        public GameObject wandHead;

        protected override void OnWandActivated()
        {
            ChangeWandStatus(true);
            base.OnWandActivated();
        }

        protected override void OnWandDisabled()
        {
            ChangeWandStatus(false);
            base.OnWandDisabled();
        }

        private void ChangeWandStatus(bool isActive)
        {
            spotLight.SetActive(isActive);
            wandHead.SetActive(isActive);
        }
    }
}