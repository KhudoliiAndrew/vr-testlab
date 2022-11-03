using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace outline
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  public class Outline : MonoBehaviour {

    [SerializeField]
    private Color outlineColor = Color.white;

    [SerializeField, Range(0f, 20f)]
    private float outlineWidth = .5f;

    private Material _outlineMaskMaterial;
    private Material _outlineFillMaterial;

    private bool _needsUpdate = true;
    void Awake() {
      // Instantiate outline materials
      _outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Shaders/outline/variant1/2DOutlineMask"));
      _outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Shaders/outline/variant1/2DOutline"));

      _outlineMaskMaterial.name = "OutlineMask (Instance)";
      _outlineFillMaterial.name = "OutlineFill (Instance)";
    
    }

    void OnEnable() {
      foreach (var renderer in GetComponentsInChildren<Renderer>()) {

        // Append outline shaders
        var materials = renderer.sharedMaterials.ToList();

        if(!IsHaveMaterials(materials, _outlineMaskMaterial.name))
          materials.Add(_outlineMaskMaterial);
      
        if(!IsHaveMaterials(materials, _outlineFillMaterial.name))
          materials.Add(_outlineFillMaterial);

        renderer.materials = materials.ToArray();
      }
    
      UpdateMaterialProperties();
    }


    void Update() {
      UpdateMaterialProperties();

      if (_needsUpdate) {
        _needsUpdate = false;

      }
    }
    void OnDisable() {
      foreach (var renderer in GetComponentsInChildren<Renderer>()) {

        // Remove outline shaders
        var materials = renderer.sharedMaterials.ToList();

        materials.Remove(_outlineMaskMaterial);
        materials.Remove(_outlineFillMaterial);

        renderer.materials = materials.ToArray();
      }
    }

    void OnDestroy() {

      // Destroy material instances
      DestroyImmediate(_outlineMaskMaterial);
      DestroyImmediate(_outlineFillMaterial);
    }

    void UpdateMaterialProperties() {

      // Apply properties according to mode
      _outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

      _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
      _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
      _outlineFillMaterial.SetFloat("_OutlineThickness", outlineWidth);
    }

    bool IsHaveMaterials( List<Material> materials, String tragetName)
    {
      for (int i = 0; i < materials.Count; i++)
      {
        Material material = materials[i];

        if (material.name == tragetName) return true;
      }

      return false;

    }
  }
}
