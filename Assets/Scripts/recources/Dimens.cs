using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Dimens : MonoBehaviour
{
    [Range(0f, 10f)] public const float OutlineWidth = 5f;

    [Range(0f, 3f)] public const float FastDuration = 0.25f;

    public static readonly Color OutlineSelectedColor = Color.white;
    public static readonly Color OutlineHoverColor = Color.gray;

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
        Shader.SetGlobalFloat(Constants.OutlineMaxWidthName, OutlineWidth);
    }
}
