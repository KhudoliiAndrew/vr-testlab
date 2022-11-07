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
        public Color outlineColor = Color.white;
        
        private bool _lastVisibleState;
        private Color _lastOutlineColor;

        private void Update()
        {
            if (_lastVisibleState != isVisible)
            {
                StartCoroutine(UpdateState(isVisible));
                _lastVisibleState = isVisible;
            }
            
            if (_lastOutlineColor != outlineColor)
            {
                StartCoroutine(UpdateColor(outlineColor));
                _lastOutlineColor = outlineColor;
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
        
        private IEnumerator UpdateColor(Color color)
        {
            const float duration = Dimens.FastDuration;
            var timer = 0f;

            while (timer < duration)
            {
                var startColor = GetOutlineColor();

                SetOutlineColor(Color.Lerp(startColor, color, Time.deltaTime / (duration - timer)));
                
                timer += Time.deltaTime;
                yield return null;
            }
        }
        
        
        private void SetOutlineColor(Color color)
        {
            gameObject.GetComponent<OutlineSettings>().outlineFillMaterial.SetColor(Constants.OutlineColorName, color);
        }

        private Color GetOutlineColor()
        {
            var value = gameObject.GetComponent<OutlineSettings>().outlineFillMaterial
                .GetColor(Constants.OutlineColorName);

            return value;
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