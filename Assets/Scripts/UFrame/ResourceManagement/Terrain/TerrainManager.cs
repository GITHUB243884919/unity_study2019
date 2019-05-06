using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
using UFrame.Data;

namespace UFrame.ResourceManagement
{
    public class TerrainManager : Singleton<TerrainManager>, ISingleton
    {
        GameObject terrainRoot;
        Transform terrainRootTrans;
        TerrainSlicingData slicingData;
        Vector2 mapTilesize;
        Vector2 trunkSize;
        /// <summary>
        /// trunk是9宫格，边长为3
        /// </summary>
        int trunkEdgeNum = 3;
        int trunkOffset = 1;
        Dictionary<int, GameObject> trunkDic = new Dictionary<int, GameObject>();
        List<int> unloadTrunkLst = new List<int>();

        public void Init()
        {

        }

        public void SetTrunkEdgeNum(int trunkEdgeNum)
        {
            this.trunkEdgeNum = trunkEdgeNum;
            trunkOffset = trunkEdgeNum / 2;
        }

        public void LoadSlicingMapTileAsync(string terrainName, Vector3 pos)
        {
            LoadTerrainRoot();
            LoadSlicingData(terrainName);
            LoadTrunkAsync(pos);
        }

        void LoadTerrainRoot()
        {
            if (terrainRoot != null)
            {
                return;
            }

            var getter = ResHelper.LoadGameObject("terrainslicing/terrrain_root");
            terrainRoot = getter.Get();
            terrainRootTrans = terrainRoot.transform;
        }

        void LoadSlicingData(string terrainName)
        {
            if (slicingData != null)
            {
                return;
            }
            string slicingDataPath = string.Format("terrainslicing/{0}/{1}_slicingdata", terrainName, terrainName);
            var getter = ResHelper.LoadAsset(slicingDataPath);
            slicingData = getter.Get(terrainRoot) as TerrainSlicingData;
            trunkSize = new Vector2(slicingData.terrainSize.x / slicingData.slicingSize, slicingData.terrainSize.z / slicingData.slicingSize);
            mapTilesize = new Vector2(slicingData.terrainSize.x, slicingData.terrainSize.z);
            Logger.LogWarp.LogFormat("{0}, {1}", mapTilesize, trunkSize);
        }

        void LoadTrunkAsync(Vector3 pos)
        {
            int NonNullCount = trunkDic.GetNonNullCount();//GetTrunkDicNonCount();
            if (NonNullCount < (trunkEdgeNum * trunkEdgeNum) && NonNullCount != 0)
            {
                return;
            }

            Vector2_Bit idx = LocateTrunk(pos);
            //加载当前的N宫格
            LoadCurrNTrunk(idx);
            //释放之前N宫格不在当前N宫格内的trunk
            UnLoadPreNTrunk(idx);
        }

        /// <summary>
        /// 定位到trunk
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        Vector2_Bit LocateTrunk(Vector3 pos)
        {
            //ceil是上取整， 从0开始计数，所以-1
            int x = Mathf.CeilToInt(pos.x / trunkSize.x);
            int y = Mathf.CeilToInt(pos.z / trunkSize.y);
            return new Vector2_Bit(x - 1, y - 1);
        }

        /// <summary>
        /// 加载N宫格
        /// </summary>
        /// <param name="idx"></param>
        void LoadCurrNTrunk(Vector2_Bit idx)
        {
            for (int i = 0; i < trunkEdgeNum; i++)
            {
                for (int j = 0; j < trunkEdgeNum; j++)
                {
                    //idx 是N宫格的中心，idx.x - trunkOffset 是左下角
                    Vector2_Bit idxTrunk = new Vector2_Bit(i + idx.x - trunkOffset, j + idx.y - trunkOffset);
                    string path = string.Format("terrainslicing/{0}/{1}_{2}_{3}",
                        slicingData.terrainName, slicingData.terrainName,
                        idxTrunk.x, idxTrunk.y);
                    GameObject trunkGo = null;
                    if (!trunkDic.TryGetValue(idxTrunk.BitData, out trunkGo))
                    {
                        trunkDic.Add(idxTrunk.BitData, null);
                        ResHelper.LoadGameObjectAsync(path, (getter) =>
                        {
                            trunkGo = getter.Get();
                            float x = trunkSize.x * idxTrunk.x;
                            float y = trunkSize.y * idxTrunk.y;

                            trunkGo.transform.position = new Vector3(x, 0, y);

                            trunkGo.transform.SetParent(terrainRootTrans);
                            trunkDic[idxTrunk.BitData] = trunkGo;
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 释放之前N宫格不在当前N宫格内的trunk
        /// 因为是异步加载，可能本次释放不完。
        /// </summary>
        /// <param name="idx"></param>
        void UnLoadPreNTrunk(Vector2_Bit idx)
        {
            int x = idx.x;
            int y = idx.y;
            if (trunkDic.GetNonNullCount() <= (trunkEdgeNum * trunkEdgeNum))
            {
                return;
            }

            unloadTrunkLst.Clear();
            foreach (var kv in trunkDic)
            {
                Vector2_Bit preIdx = new Vector2_Bit(kv.Key);
                int preX = preIdx.x;
                int preY = preIdx.y;

                if (Mathf.Abs(preX - x) > trunkOffset || Mathf.Abs(preY - y) > trunkOffset)
                {
                    //Debug.Log(preIdx);
                    if (kv.Value != null)
                    {
                        ResHelper.DestroyGameObject(kv.Value);
                        unloadTrunkLst.Add(kv.Key);
                    }
                }
            }

            for (int i = 0; i < unloadTrunkLst.Count; i++)
            {
                trunkDic.Remove(unloadTrunkLst[i]);
            }

            //释放bundle
            ResHelper.RealseAllUnUse();
        }

    }
}

