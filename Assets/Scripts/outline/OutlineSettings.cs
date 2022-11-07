using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace outline
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class OutlineSettings : MonoBehaviour
    {
        [HideInInspector] public Material outlineMaskMaterial;
        [HideInInspector] public Material outlineFillMaterial;

        void Awake()
        {
            // Instantiate outline materials
            outlineMaskMaterial = Instantiate(Resources.Load<Material>(ResourcesPaths.OutlineMaskShader));
            outlineFillMaterial = Instantiate(Resources.Load<Material>(ResourcesPaths.OutlineShader));

            outlineMaskMaterial.name = Constants.PrefabOutlineMaskName;
            outlineFillMaterial.name = Constants.PrefabOutlineFillName;
        }

        void OnEnable()
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                // Append outline shaders
                var materials = renderer.sharedMaterials.ToList();

                if (!IsHaveMaterials(materials, outlineMaskMaterial.name))
                    materials.Add(outlineMaskMaterial);

                if (!IsHaveMaterials(materials, outlineFillMaterial.name))
                    materials.Add(outlineFillMaterial);

                renderer.materials = materials.ToArray();
            }

            if (gameObject.GetComponent<OutlineController>() == null)
                gameObject.AddComponent<OutlineController>();
            else
                gameObject.GetComponent<OutlineController>().enabled = true;

            UpdateMaterialProperties();
        }

        void Update()
        {
            UpdateMaterialProperties();
        }

        void OnDisable()
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                // Remove outline shaders
                var materials = renderer.sharedMaterials.ToList();

                materials.Remove(outlineMaskMaterial);
                materials.Remove(outlineFillMaterial);

                renderer.materials = materials.ToArray();
            }

            gameObject.GetComponent<OutlineController>().enabled = false;
        }

        void OnDestroy()
        {
            // Destroy material instances
            DestroyImmediate(outlineMaskMaterial);
            DestroyImmediate(outlineFillMaterial);
            DestroyImmediate(gameObject.GetComponent<OutlineController>());
        }

        void UpdateMaterialProperties()
        {
            outlineMaskMaterial.SetFloat(Constants.ShaderZTestName,
                (float)UnityEngine.Rendering.CompareFunction.Greater);
            outlineFillMaterial.SetFloat(Constants.ShaderZTestName,
                (float)UnityEngine.Rendering.CompareFunction.Always);
        }

        bool IsHaveMaterials(List<Material> materials, String targetName)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                Material material = materials[i];

                if (material.name == targetName) return true;
            }

            return false;
        }
    }
}