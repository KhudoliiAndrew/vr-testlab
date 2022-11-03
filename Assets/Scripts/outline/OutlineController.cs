using System.Collections;
using UnityEngine;

namespace outline
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class OutlineController : MonoBehaviour
    {
        public bool isVisible = false;

        [HideInInspector] public Material outlineFillMaterial;

        private bool _lastVisibleState = false;

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
            Debug.Log("here");
            const float duration = Dimens.FastDuration;
            var timer = 0f;

            while (timer < duration)
            {
                var startPos = outlineFillMaterial.GetFloat("_2DOutlineWidth");

                Debug.Log(gameObject.name + "before:" + startPos);
                float step = Dimens.OutlineWidth * Time.deltaTime / duration;

                outlineFillMaterial.SetFloat("_2DOutlineWidth",
                    isActive ? 5 : 0);

                Debug.Log(gameObject.name + "after:" + outlineFillMaterial.GetFloat("_2DOutlineWidth"));

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}