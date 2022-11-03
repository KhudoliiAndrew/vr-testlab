using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Dimens : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float outlineWidth = 3f;

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
        Shader.SetGlobalFloat("_2DOutlineWidthMax", outlineWidth);
    }
}
