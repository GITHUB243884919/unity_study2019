﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    /// <summary>
    /// 限制
    /// 禁用GameObject.Instance 无法跟踪引用
    /// 禁用GameObject.Destroy  无法移除引用
    /// 
    /// 没有销毁一个资源对应的bundle的接口，而是提供销毁所有没有使用的资源对应的接口
    /// 
    /// AssetHolder记录每个资源（包括未实例化的GameObject资源）的宿主GameObject
    /// BundleHolder记录每个bundle被引用的资源
    /// 
    /// LoadAllAssets的限制，只能LoadAll资源类型（非GameObject类型资源），以整体的形式计数，使用公共Gameobject做为持有者
    /// 
    /// 同一个目录下资源不能同名，开发环境是用AseetDatabase，底层API需要扩展名。
    /// 一个bundle下的资源不能同名
    /// 所有的资源目录，名称只能小写，用下划线分隔，安卓和ios大小写敏感，容易在PC上没事，换到移动平台出问题
    /// 
    /// </summary>
    public partial class BundleLoader : ResourceLoader
    {
        AssetBundleManifest manifest;

        string innerBundleRootPath = "";
        string outerBundleRootPath = "";

        /// <summary>
        /// asset和bundle的map关系
        /// </summary>
        Dictionary<string, string> assetMap = new Dictionary<string, string>();

        /// <summary>
        /// 资源名称对应AssetHolder
        /// </summary>
        Dictionary<string, AssetHolder> nameAssetHolders = new Dictionary<string, AssetHolder>();

        /// <summary>
        /// 引用的Go对应HashSet<AssetHolder>，销毁的时候用
        /// </summary>
        Dictionary<GameObject, HashSet<AssetHolder>> goAssetHolders = new Dictionary<GameObject, HashSet<AssetHolder>>();

        /// <summary>
        /// Bundle名称对应BundleHolder
        /// </summary>
        Dictionary<string, BundleHolder> bundleHolders = new Dictionary<string, BundleHolder>();

        /// <summary>
        /// 无用资源，每次清理时候重新构建一次
        /// </summary>
        List<string> unUseAssets = new List<string>();

        List<GameObject> unUseGameObject = new List<GameObject>();

        public enum E_LoadAsset
        {
            LoadSingle,
            LoadAll,
        }

        //static BundleLoader instance;

        //public static BundleLoader GetInstance()
        //{
        //    return instance;
        //} 

        public override void Init()
        {
            //this.resLoader = resLoader;
            innerBundleRootPath = Application.streamingAssetsPath + "/Bundles/";
            outerBundleRootPath = Application.persistentDataPath + "/Bundles/";
            Loadmanifest();
            LoadAssetMap();

            //instance = this;
        }

        void Loadmanifest()
        {
            string bundlePath = GetBundlePath("Bundles");
            var bundle = AssetBundle.LoadFromFile(bundlePath);

            manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            bundle.Unload(false);
        }

        void LoadAssetMap()
        {
            string bundlePath = GetBundlePath("asset-bundle");
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var txt = bundle.LoadAsset<TextAsset>("asset-bundle");
            string strTxt = txt.text;

#if UNITY_EDITOR
            string[] line = strTxt.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
#else
            string[] line = strTxt.Split(new char[] {'\n' }, System.StringSplitOptions.RemoveEmptyEntries);
#endif
            for (int i = 0; i < line.Length; ++i)
            {
                //Debug.LogError("[" + line[i] + "]");
                string[] temp = line[i].Split(',');
                for(int j = 0; j < temp.Length; ++j)
                {
                    //Debug.LogError("{" + temp[j] + "}");
                }
                assetMap.Add(temp[0], temp[1]);

            }
            bundle.Unload(true);
        }

        string GetBundlePath(string bundleName)
        {
            string outerPath = Path.Combine(outerBundleRootPath, bundleName);
            string innerPath = Path.Combine(innerBundleRootPath, bundleName);

            if (File.Exists(outerPath))
            {
                return outerPath;
            }

            return innerPath;
        }

        string GetBundleName(string assetName)
        {
            string result;
            assetMap.TryGetValue(assetName, out result);
            return result;
        }


#region 释放接口
        /// <summary>
        /// 释放接口，释放所有未被引用的资源。
        /// 1.把引用计数为0的资源引用取出。本类的DestroyXX减少资源引用计数。
        /// 2.然后看看是否能把对应的bundle能释放 
        /// </summary>
        public override void RealseAllUnUse()
        {
            //unUseGameObject.Clear();
            //bool could = true;
            //foreach(var kv in goAssetHolders)
            //{
            //    could = true;
            //    HashSet<AssetHolder> assetholders = kv.Value;
            //    foreach(var item in assetholders)
            //    {
            //        if (!item.CouldRealse())
            //        {
            //            could = false;
            //            break;
            //        }
            //    }
            //    if (could)
            //    {
            //        unUseGameObject.Add(kv.Key);
            //    }
            //}
            
            //for(int i = 0, iMax = unUseGameObject.Count; i < iMax; ++i)
            //{
            //    goAssetHolders[unUseGameObject[i]].Clear();
            //    goAssetHolders.Remove(unUseGameObject[i]);
            //}

            unUseAssets.Clear();
            foreach (var kv in nameAssetHolders)
            {
                if (kv.Value.CouldRealse())
                {
                    unUseAssets.Add(kv.Key);
                }
            }

            for (int i = 0, iMax = unUseAssets.Count; i < iMax; ++i)
            {
                string bundleName = GetBundleName(unUseAssets[i]);
                BundleHolder bundleHolder = null;
                if (bundleHolders.TryGetValue(bundleName, out bundleHolder))
                {
                    bundleHolder.RemoveRefence(unUseAssets[i]);
                    if (bundleHolder.CouldRealse())
                    {
                        bundleHolder.Release();
                        bundleHolders.Remove(bundleName);
                    }
                }

                string[] dependencies = this.manifest.GetAllDependencies(bundleName);
                for (int k = 0, kMax = dependencies.Length; k < kMax; ++k)
                {
                    BundleHolder dependBundleHolder = null;
                    if (bundleHolders.TryGetValue(dependencies[k], out dependBundleHolder))
                    {
                        dependBundleHolder.RemoveRefence(unUseAssets[i]);
                        if (dependBundleHolder.CouldRealse())
                        {
                            dependBundleHolder.Release();
                            bundleHolders.Remove(dependencies[k]);
                        }
                    }
                }

                nameAssetHolders[unUseAssets[i]].Release();
                nameAssetHolders.Remove(unUseAssets[i]);
            }
        }

        public override void DestroyGameObject(GameObject go)
        {
            //1.去资源引用
            foreach (var item in goAssetHolders[go])
            {
                item.RemoveRefence(go);
            }
            ////已经销毁的对象在维护完资源引用后不用再持有
            goAssetHolders[go].Clear();
            goAssetHolders.Remove(go);


            //2.销毁对象
            GameObject.Destroy(go);
        }

        public override void RealseAsset(AssetHolder assetHolder, GameObject go)
        {
            //1.去资源引用
            assetHolder.RemoveRefence(go);

        }
#endregion


        /// <summary>
        /// 维护资源和GameObject的引用关系
        /// </summary>
        /// <param name="go"></param>
        /// <param name="assetHolder"></param>
        public override void AddGameObjectAssetHolder(GameObject go, AssetHolder assetHolder)
        {
            //记录资源引用
            assetHolder.AddRefence(go);

            HashSet<AssetHolder> assetHolders = null;
            if (!goAssetHolders.TryGetValue(go, out assetHolders))
            {
                assetHolders = new HashSet<AssetHolder>();
                goAssetHolders.Add(go, assetHolders);
            }
            assetHolders.Add(assetHolder);
        }

    }
}

