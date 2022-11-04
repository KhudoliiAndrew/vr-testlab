using System;
using System.Collections;
using UnityEngine;

namespace outline
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class OutlineController : MonoBehaviour
    {
        public bool isVisible;
        private bool _lastVisibleState;

        private void Update()
        {
            if (_lastVisibleState != isVisible)
            {
                StartCoroutine(UpdateState(isVisible));
                _lastVisibleState = isVisible;
            }
        }

        private IEnumerator UpdateState(bool isActive)
        {
            const float duration = Dimens.FastDuration;
            var timer = 0f;

            while (timer < duration)
            {
                var startPos = GetOutlineValue();
                float step = Dimens.OutlineWidth * Time.deltaTime / duration;

                SetOutlineValue(isActive ? startPos + step : startPos - step);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void SetOutlineValue(float value)
        {
            gameObject.GetComponent<OutlineSettings>().outlineFillMaterial.SetFloat(Constants.OutlineWidthName, value);
        }

        private float GetOutlineValue()
        {
            var value = gameObject.GetComponent<OutlineSettings>().outlineFillMaterial
                .GetFloat(Constants.OutlineWidthName);

            return Math.Clamp(value, 0, Dimens.OutlineWidth);
        }
    }
}