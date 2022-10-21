//
//  Outline.cs
//  QuickOutline
//
//  Created by Chris Nolet on 3/30/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Outline : MonoBehaviour {

  public Color OutlineColor {
    get { return outlineColor; }
    set {
      outlineColor = value;
      _needsUpdate = true;
    }
  }

  public float OutlineWidth {
    get { return outlineWidth; }
    set {
      outlineWidth = value;
      _needsUpdate = true;
    }
  }

  [Serializable]
  private class ListVector3 {
    public List<Vector3> data;
  }

  [SerializeField]
  private Color outlineColor = Color.white;

  [SerializeField, Range(0f, 20f)]
  private float outlineWidth = .5f;

  private Renderer[] _renderers;
  private Material _outlineMaskMaterial;
  private Material _outlineFillMaterial;

  private bool _needsUpdate = true;
  void Awake() {

    // Cache renderers
    _renderers = GetComponentsInChildren<Renderer>();

    // Instantiate outline materials
    _outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Shaders/outline/variant1/OutlineMask"));
    _outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Shaders/outline/variant1/2DOutline"));

    _outlineMaskMaterial.name = "OutlineMask (Instance)";
    _outlineFillMaterial.name = "OutlineFill (Instance)";
    
  }

  void OnEnable() {
    foreach (var renderer in _renderers) {

      // Append outline shaders
      var materials = renderer.sharedMaterials.ToList();

      materials.Add(_outlineMaskMaterial);
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
    foreach (var renderer in _renderers) {

      // Remove outline shaders
      var materials = renderer.sharedMaterials.ToList();

      materials.Remove(_outlineMaskMaterial);
      materials.Remove(_outlineFillMaterial);

      renderer.materials = materials.ToArray();
    }
  }

  void OnDestroy() {

    // Destroy material instances
    Destroy(_outlineMaskMaterial);
    Destroy(_outlineFillMaterial);
  }

  void UpdateMaterialProperties() {

    // Apply properties according to mode
    _outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
    _outlineFillMaterial.SetFloat("_OutlineThickness", outlineWidth);
  }
}
