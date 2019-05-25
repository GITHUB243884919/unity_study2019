using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace UFrame.ResourceManagement
{
    public class TerrainSlicing : Editor
    {
        public static string terrainDataSavePath = "Assets/GameResources/terrainslicing_data";
        public static string terrainGoSavePath = "Assets/GameResources/terrainslicing";
        public static string terrainName = "";
        //分割大小
        public static int SLICING_SIZE = 8;

        //开始分割地形
        [MenuItem("UFrame框架/地形/切割")]
        private static void Slicing()
        {
            Terrain terrain = GameObject.FindObjectOfType<Terrain>();
            if (terrain == null)
            {
                Debug.LogError("找不到地形!");
                return;
            }
            terrainName = terrain.name;

            if (!Directory.Exists(terrainGoSavePath))
            {
                Directory.CreateDirectory(terrainGoSavePath);
            }


            if (!Directory.Exists(terrainDataSavePath))
            {
                Directory.CreateDirectory(terrainDataSavePath);
            }


            if (Directory.Exists(Path.Combine(terrainGoSavePath, terrainName)))
            {
                Directory.Delete(Path.Combine(terrainGoSavePath, terrainName), true);
            }
            Directory.CreateDirectory(Path.Combine(terrainGoSavePath, terrainName));

            if (Directory.Exists(Path.Combine(terrainDataSavePath, terrainName)))
            {
                Directory.Delete(Path.Combine(terrainDataSavePath, terrainName), true);
            }
            Directory.CreateDirectory(Path.Combine(terrainDataSavePath, terrainName));


            TerrainData terrainData = terrain.terrainData;

            //这里我分割的宽和长度是一样的.这里求出循环次数,TerrainLoad.SIZE要生成的地形宽度,长度相同
            //高度地图的分辨率只能是2的N次幂加1,所以SLICING_SIZE必须为2的N次幂
            //SLICING_SIZE = (int)terrainData.size.x / TerrainLoad.SIZE;
            Vector3 orgSize = terrainData.size;
            //Debug.LogError("terrainData.size " + terrainData.size + " " +
            //    terrainData.heightmapWidth + " " + terrainData.heightmapHeight);

            //得到新地图分辨率
            int newHeightmapResolution = (terrainData.heightmapResolution - 1) / SLICING_SIZE;
            int newAlphamapResolution = terrainData.alphamapResolution / SLICING_SIZE;
            int newbaseMapResolution = terrainData.baseMapResolution / SLICING_SIZE;
            SplatPrototype[] splatProtos = terrainData.splatPrototypes;

            //循环宽和长,生成小块地形
            for (int x = 0; x < SLICING_SIZE; ++x)
            {
                for (int y = 0; y < SLICING_SIZE; ++y)
                {
                    //创建资源
                    TerrainData subTerrainData = new TerrainData();
                    string terrainPrefabPath = string.Format("{0}/{1}/{2}_{3}_{4}.prefab", terrainGoSavePath, terrainName, terrainName, x, y);
                    string terrainDataPath = string.Format("{0}/{1}/{2}_{3}_{4}.asset", terrainDataSavePath, terrainName, terrainName, x, y);
                    AssetDatabase.CreateAsset(subTerrainData, terrainDataPath);
                    EditorUtility.DisplayProgressBar("正在分割地形", terrainPrefabPath, (float)(x * SLICING_SIZE + y) / (float)(SLICING_SIZE * SLICING_SIZE));

                    //设置分辨率参数
                    subTerrainData.heightmapResolution = (terrainData.heightmapResolution - 1) / SLICING_SIZE;
                    subTerrainData.alphamapResolution = terrainData.alphamapResolution / SLICING_SIZE;
                    subTerrainData.baseMapResolution = terrainData.baseMapResolution / SLICING_SIZE;

                    //设置大小
                    subTerrainData.size = new Vector3(orgSize.x / SLICING_SIZE, orgSize.y, orgSize.z / SLICING_SIZE);

                    //设置地形原型
                    SplatPrototype[] subSplats = new SplatPrototype[splatProtos.Length];
                    for (int i = 0; i < splatProtos.Length; ++i)
                    {
                        subSplats[i] = new SplatPrototype();
                        subSplats[i].metallic = splatProtos[i].metallic;
                        subSplats[i].smoothness = splatProtos[i].smoothness;
                        subSplats[i].specular = splatProtos[i].specular;
                        subSplats[i].texture = splatProtos[i].texture;
                        subSplats[i].normalMap = splatProtos[i].normalMap;

                        subSplats[i].tileSize = splatProtos[i].tileSize;
                        float offsetX = (subTerrainData.size.x * x) % splatProtos[i].tileSize.x + splatProtos[i].tileOffset.x;
                        float offsetY = (subTerrainData.size.z * y) % splatProtos[i].tileSize.y + splatProtos[i].tileOffset.y;
                        subSplats[i].tileOffset = new Vector2(offsetX, offsetY);
                    }
                    subTerrainData.splatPrototypes = subSplats;


                    //设置混合贴图
                    float[,,] alphamap = new float[newAlphamapResolution, newAlphamapResolution, splatProtos.Length];
                    alphamap = terrainData.GetAlphamaps(x * subTerrainData.alphamapWidth, y * subTerrainData.alphamapHeight, subTerrainData.alphamapWidth, subTerrainData.alphamapHeight);
                    subTerrainData.SetAlphamaps(0, 0, alphamap);

                    //设置高度
                    int xBase = terrainData.heightmapWidth / SLICING_SIZE;
                    int yBase = terrainData.heightmapHeight / SLICING_SIZE;
                    float[,] height = terrainData.GetHeights(xBase * x, yBase * y, xBase + 1, yBase + 1);
                    subTerrainData.SetHeights(0, 0, height);

                    //存储分块到prefab
                    GameObject subTerrainGo = new GameObject();
                    var subTerrainCom = subTerrainGo.AddComponent<Terrain>();
                    subTerrainCom.terrainData = subTerrainData;
                    var subCollider = subTerrainGo.AddComponent<TerrainCollider>();
                    subCollider.terrainData = subTerrainData;
                    subTerrainGo.name = Path.GetFileNameWithoutExtension(terrainPrefabPath);
                    PrefabUtility.CreatePrefab(terrainPrefabPath, subTerrainGo, ReplacePrefabOptions.ConnectToPrefab);
                    GameObject.DestroyImmediate(subTerrainGo);

                }
            }


            var slicingData = CreateInstance<TerrainSlicingData>();
            slicingData.terrainName = terrainName;
            slicingData.slicingSize = SLICING_SIZE;
            slicingData.terrainSize = orgSize;
            string slicingDataPath = Path.Combine(terrainGoSavePath, terrainName);
            slicingDataPath = string.Format("{0}/{1}_slicingdata.asset", slicingDataPath, terrainName);
            AssetDatabase.CreateAsset(slicingData, slicingDataPath);
            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayDialog("", "地形切割完毕", "确定");
        }
    }
}
