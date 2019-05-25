using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Shader_Custom_Add
{
    [MenuItem("UFrame框架/地形/相关shader To AlwaysIncluded", false, 11)]
    public static void TerrainShaderAllwaysIncluded()
    {
        string[] terrainShaders = new string[]
        {
            "Nature/Terrain/Standard",
            "Nature/Terrain/Specular",
            "Nature/Terrain/Diffuse",
            "Legacy Shaders/Specular",
            "Hidden/TerrainEngine/Splatmap/Standard-Base",
            "Hidden/TerrainEngine/Splatmap/Standard-AddPass",
            "Hidden/TerrainEngine/Splatmap/Specular-Base",
            "Hidden/TerrainEngine/Splatmap/Specular-AddPass",
            "Hidden/TerrainEngine/Splatmap/Diffuse-AddPass",
            "Hidden/TerrainEngine/Details/WavingDoublePass",
            "Hidden/TerrainEngine/Details/Vertexlit",
            "Hidden/TerrainEngine/Details/BillboardWavingDoublePass",
            "Hidden/TerrainEngine/CameraFacingBillboardTree",
            "Hidden/TerrainEngine/BillboardTree",
            "Hidden/Nature/Tree Soft Occlusion Leaves Rendertex",
            "Hidden/Nature/Tree Soft Occlusion Bark Rendertex"
        };
        SerializedObject graphicsSettings = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/GraphicsSettings.asset")[0]);
        SerializedProperty it = graphicsSettings.GetIterator();
        SerializedProperty dataPoint;
        while (it.NextVisible(true))
        {
            if (it.name == "m_AlwaysIncludedShaders")
            {
                //it.ClearArray();
                for (int i = 0; i < terrainShaders.Length; i++)
                {
                    it.InsertArrayElementAtIndex(i);
                    dataPoint = it.GetArrayElementAtIndex(i);
                    dataPoint.objectReferenceValue = Shader.Find(terrainShaders[i]);
                }
                graphicsSettings.ApplyModifiedProperties();
            }
        }
    }

  
}
