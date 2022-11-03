using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Dimens : MonoBehaviour
{
    [Range(0f, 10f)] public const float OutlineWidth = 3f;

    [Range(0f, 3f)] public const float FastDuration = 1.5f;

    void Awake()
    {
        UpdateShadersProperties();
    }
    void OnEnable()
    {
        UpdateShadersProperties();
    }


    void Update()
    {
        UpdateShadersProperties();

    }

    private void UpdateShadersProperties()
    {
        Shader.SetGlobalFloat("_2DOutlineWidthMax", OutlineWidth);
    }
}
