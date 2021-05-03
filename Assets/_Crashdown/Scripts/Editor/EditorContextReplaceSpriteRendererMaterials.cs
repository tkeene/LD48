#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorContextReplaceSpriteRendererMaterials
{
    const string kMaterialToUse = "CrashdownAlphaCutoffSpriteMaterial";

    [MenuItem("Crashdown/Update Prefab SpriteRenderer Materials")]
    public static void ReplaceMaterials()
    {
        string materialGuid = AssetDatabase.FindAssets(kMaterialToUse)[0];
        string materialPath = AssetDatabase.GUIDToAssetPath(materialGuid);
        Material newMaterial = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

        foreach (Object selectedPrefabObject in Selection.objects)
        {
            GameObject selectedPrefab = selectedPrefabObject as GameObject;
            foreach (SpriteRenderer spriteRenderer in selectedPrefab.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.material = newMaterial;
                spriteRenderer.transform.localPosition = new Vector3(
                    spriteRenderer.transform.localPosition.x,
                    0.2f,
                    spriteRenderer.transform.localPosition.z);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif