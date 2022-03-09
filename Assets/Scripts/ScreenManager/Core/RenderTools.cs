using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class RenderTools
{
    /// <summary>
    /// Reloads shaders for all objects in specified game object
    /// </summary>
    /// <param name="obj">Root game object to start shader reload process</param>
    public static void RefreshMaterialsRecursive(GameObject obj){
#if UNITY_EDITOR
        if (null == obj)
        {
            return;
        }

        foreach (Transform child in obj.transform){
            if (null == child)
            {
                continue;
            }

            var rend = child.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.shader = Shader.Find(rend.material.shader.name);
            }

            var image = child.GetComponent<Image>();
            if (image != null)
            {
                image.material.shader = Shader.Find(image.material.shader.name);
            }

            var textMeshPro = child.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                if (textMeshPro != null && textMeshPro.spriteAsset != null && textMeshPro.spriteAsset.material != null)
                {
                    textMeshPro.spriteAsset.material.shader = Shader.Find(textMeshPro.spriteAsset.material.shader.name);
                }

                if (textMeshPro != null && textMeshPro.font != null && textMeshPro.font.material != null)
                {
                    textMeshPro.font.material.shader = Shader.Find(textMeshPro.font.material.shader.name);
                }

                if (textMeshPro != null && textMeshPro.fontMaterial != null)
                {
                    textMeshPro.fontMaterial.shader = Shader.Find(textMeshPro.fontMaterial.shader.name);
                }

                foreach (var mat in textMeshPro.fontMaterials)
                {
                    mat.shader = Shader.Find(mat.shader.name);
                }

                foreach (var mat in textMeshPro.fontSharedMaterials)
                {
                    mat.shader = Shader.Find(mat.shader.name);
                }

                textMeshPro.material.shader = Shader.Find(textMeshPro.material.shader.name);
            }

            RefreshMaterialsRecursive(child.gameObject);
        }

#endif
    }
}