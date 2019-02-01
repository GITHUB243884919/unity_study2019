using System.Collections;
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
    /// </summary>
    public partial class BundleLoader : MonoBehaviour
    {
        //ResourceLoader resLoader;
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

        static BundleLoader instance;
        public static BundleLoader GetInstance()
        {
            return instance;
        } 

        void Awake()
        {
            //this.resLoader = resLoader;
            innerBundleRootPath = Application.streamingAssetsPath + "/Bundles/";
            outerBundleRootPath = Application.persistentDataPath + "/Bundles/";
            Loadmanifest();
            LoadAssetMap();


            instance = this;
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
        public void RealseAllUnUse()
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

        public void DestroyGameObject(GameObject go)
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

        public void RealseAsset(AssetHolder assetHolder, GameObject go)
        {
            //1.去资源引用
            assetHolder.RemoveRefence(go);

        }
#endregion

#region 同步


        //public G LoadAll<G>(string assetName)
        //    where G : IAssetGetter, new()
        //{
        //    // 1 从nameAssetHolders获取资源
        //    AssetHolder assetHolder = null;
        //    //T t = default(T);
        //    string bundleName = GetBundleName(assetName);
        //    //HashSet<AssetHolder> assetHolders = null;
        //    string[] dependencies = null;
        //    G getter = new G();
        //    if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
        //    {
        //        //t = assetHolder.Get<T>();
        //        //记录资源引用
        //        //assetHolder.AddRefence(go);
        //        //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //        //{
        //        //    assetHolders = new HashSet<AssetHolder>();
        //        //    goAssetHolders.Add(go, assetHolders);
        //        //}
        //        //assetHolders.Add(assetHolder);

        //        getter.SetAssetHolder(assetHolder);

        //        //记录Bundle引用
        //        //nameAssetHolders有,那么bundleHolders必有
        //        bundleHolders[bundleName].AddRefence(assetName);
        //        dependencies = manifest.GetAllDependencies(bundleName);
        //        for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //        {
        //            bundleHolders[dependencies[i]].AddRefence(assetName);
        //        }
        //        //return assetHolder;
        //        return getter;
        //    }

        //    //2 从bundleHolders获取资源
        //    AssetBundle bundle = null;
        //    BundleHolder bundleHolder = null;
        //    // 没有加载过bundle
        //    if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
        //    {
        //        bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
        //        //存bundleHolder
        //        bundleHolder = new BundleHolder(bundle);
        //        bundleHolder.AddRefence(assetName);
        //        bundleHolders.Add(bundleName, bundleHolder);
        //    }
        //    else
        //    {
        //        bundleHolder.AddRefence(assetName);
        //        bundle = bundleHolder.Get();
        //    }


        //    // 加载依赖的bundle
        //    dependencies = manifest.GetAllDependencies(bundleName);
        //    for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //    {
        //        BundleHolder dependBundleHolder = null;
        //        if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
        //        {
        //            //已经存在的bundle只增加引用
        //            dependBundleHolder.AddRefence(assetName);
        //            continue;
        //        }
        //        //没加载过的
        //        AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
        //        dependBundleHolder = new BundleHolder(dependBundle);
        //        dependBundleHolder.AddRefence(assetName);
        //        //存bundleHolder
        //        bundleHolders.Add(dependencies[i], dependBundleHolder);
        //    }

        //    //所有bundle加载完毕
        //    Object[] allObjs = bundle.LoadAllAssets();
        //    //t = callback(bundle);
        //    //新建AssetHolder
        //    assetHolder = new AssetHolder(allObjs);
        //    getter.SetAssetHolder(assetHolder);

        //    ////记录资源引用
        //    //assetHolder.AddRefence(go);
        //    //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //    //{
        //    //    assetHolders = new HashSet<AssetHolder>();
        //    //    goAssetHolders.Add(go, assetHolders);
        //    //}
        //    //assetHolders.Add(assetHolder);


        //    nameAssetHolders.Add(assetName, assetHolder);

        //    //return assetHolder;
        //    return getter;
        //}


        /// <summary>
        /// 维护资源和GameObject的引用关系
        /// </summary>
        /// <param name="go"></param>
        /// <param name="assetHolder"></param>
        public void AddGameObjectAssetHolder(GameObject go, AssetHolder assetHolder)
        {
            HashSet<AssetHolder> assetHolders = null;
            if (!goAssetHolders.TryGetValue(go, out assetHolders))
            {
                assetHolders = new HashSet<AssetHolder>();
                goAssetHolders.Add(go, assetHolders);
            }
            assetHolders.Add(assetHolder);
        }

        //public G LoadAsset<G, T>(string assetName, System.Func<AssetBundle, T> callback)
        //    where G : IAssetGetter, new()
        //    where T : Object
        //{
        //    // 1 从nameAssetHolders获取资源
        //    AssetHolder assetHolder = null;
        //    T t = default(T);
        //    string bundleName = GetBundleName(assetName);
        //    //HashSet<AssetHolder> assetHolders = null;
        //    string[] dependencies = null;
        //    G getter = new G();
        //    if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
        //    {
        //        //t = assetHolder.Get<T>();
        //        //记录资源引用
        //        //assetHolder.AddRefence(go);
        //        //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //        //{
        //        //    assetHolders = new HashSet<AssetHolder>();
        //        //    goAssetHolders.Add(go, assetHolders);
        //        //}
        //        //assetHolders.Add(assetHolder);

        //        getter.SetAssetHolder(assetHolder);

        //        //记录Bundle引用
        //        //nameAssetHolders有,那么bundleHolders必有
        //        bundleHolders[bundleName].AddRefence(assetName);
        //        dependencies = manifest.GetAllDependencies(bundleName);
        //        for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //        {
        //            bundleHolders[dependencies[i]].AddRefence(assetName);
        //        }
        //        //return assetHolder;
        //        return getter;
        //    }

        //    //2 从bundleHolders获取资源
        //    AssetBundle bundle = null;
        //    BundleHolder bundleHolder = null;
        //    // 没有加载过bundle
        //    if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
        //    {
        //        bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
        //        //存bundleHolder
        //        bundleHolder = new BundleHolder(bundle);
        //        bundleHolder.AddRefence(assetName);
        //        bundleHolders.Add(bundleName, bundleHolder);
        //    }
        //    else
        //    {
        //        bundleHolder.AddRefence(assetName);
        //        bundle = bundleHolder.Get();
        //    }


        //    // 加载依赖的bundle
        //    dependencies = manifest.GetAllDependencies(bundleName);
        //    for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //    {
        //        BundleHolder dependBundleHolder = null;
        //        if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
        //        {
        //            //已经存在的bundle只增加引用
        //            dependBundleHolder.AddRefence(assetName);
        //            continue;
        //        }
        //        //没加载过的
        //        AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
        //        dependBundleHolder = new BundleHolder(dependBundle);
        //        dependBundleHolder.AddRefence(assetName);
        //        //存bundleHolder
        //        bundleHolders.Add(dependencies[i], dependBundleHolder);
        //    }

        //    //所有bundle加载完毕
        //    //t = bundle.LoadAsset<T>(assetName);
        //    t = callback(bundle);
        //    //新建AssetHolder
        //    assetHolder = new AssetHolder(t);
        //    getter.SetAssetHolder(assetHolder);

        //    ////记录资源引用
        //    //assetHolder.AddRefence(go);
        //    //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //    //{
        //    //    assetHolders = new HashSet<AssetHolder>();
        //    //    goAssetHolders.Add(go, assetHolders);
        //    //}
        //    //assetHolders.Add(assetHolder);


        //    nameAssetHolders.Add(assetName, assetHolder);

        //    //return assetHolder;
        //    return getter;
        //}



        //public G LoadAsset<G, T>(string assetName)
        //    where G : IAssetGetter, new()
        //    where T : Object
        //{
        //    // 1 从nameAssetHolders获取资源
        //    AssetHolder assetHolder = null;
        //    T t = default(T);
        //    string bundleName = GetBundleName(assetName);
        //    //HashSet<AssetHolder> assetHolders = null;
        //    string[] dependencies = null;
        //    G getter = new G();
        //    if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
        //    {
        //        //t = assetHolder.Get<T>();
        //        //记录资源引用
        //        //assetHolder.AddRefence(go);
        //        //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //        //{
        //        //    assetHolders = new HashSet<AssetHolder>();
        //        //    goAssetHolders.Add(go, assetHolders);
        //        //}
        //        //assetHolders.Add(assetHolder);

        //        getter.SetAssetHolder(assetHolder);

        //        //记录Bundle引用
        //        //nameAssetHolders有,那么bundleHolders必有
        //        bundleHolders[bundleName].AddRefence(assetName);
        //        dependencies = manifest.GetAllDependencies(bundleName);
        //        for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //        {
        //            bundleHolders[dependencies[i]].AddRefence(assetName);
        //        }
        //        //return assetHolder;
        //        return getter;
        //    }

        //    //2 从bundleHolders获取资源
        //    AssetBundle bundle = null;
        //    BundleHolder bundleHolder = null;
        //    // 没有加载过bundle
        //    if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
        //    {
        //        bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
        //        //存bundleHolder
        //        bundleHolder = new BundleHolder(bundle);
        //        bundleHolder.AddRefence(assetName);
        //        bundleHolders.Add(bundleName, bundleHolder);
        //    }
        //    else
        //    {
        //        bundleHolder.AddRefence(assetName);
        //        bundle = bundleHolder.Get();
        //    }


        //    // 加载依赖的bundle
        //    dependencies = manifest.GetAllDependencies(bundleName);
        //    for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //    {
        //        BundleHolder dependBundleHolder = null;
        //        if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
        //        {
        //            //已经存在的bundle只增加引用
        //            dependBundleHolder.AddRefence(assetName);
        //            continue;
        //        }
        //        //没加载过的
        //        AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
        //        dependBundleHolder = new BundleHolder(dependBundle);
        //        dependBundleHolder.AddRefence(assetName);
        //        //存bundleHolder
        //        bundleHolders.Add(dependencies[i], dependBundleHolder);
        //    }

        //    //所有bundle加载完毕
        //    t = bundle.LoadAsset<T>(assetName);

        //    //新建AssetHolder
        //    assetHolder = new AssetHolder(t);
        //    getter.SetAssetHolder(assetHolder);

        //    ////记录资源引用
        //    //assetHolder.AddRefence(go);
        //    //if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //    //{
        //    //    assetHolders = new HashSet<AssetHolder>();
        //    //    goAssetHolders.Add(go, assetHolders);
        //    //}
        //    //assetHolders.Add(assetHolder);


        //    nameAssetHolders.Add(assetName, assetHolder);

        //    //return assetHolder;
        //    return getter;
        //}
        //public AssetHolder LoadAsset<T>(string assetName, GameObject go) where T : Object
        //{
        //    // 1 从nameAssetHolders获取资源
        //    AssetHolder assetHolder = null;
        //    T t = default(T);
        //    string bundleName = GetBundleName(assetName);
        //    HashSet<AssetHolder> assetHolders = null;
        //    string[] dependencies = null;
        //    if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
        //    {
        //        t = assetHolder.Get<T>();
        //        //记录资源引用
        //        assetHolder.AddRefence(go);
        //        if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //        {
        //            assetHolders = new HashSet<AssetHolder>();
        //            goAssetHolders.Add(go, assetHolders);
        //        }
        //        assetHolders.Add(assetHolder);

        //        //记录Bundle引用
        //        //nameAssetHolders有,那么bundleHolders必有
        //        bundleHolders[bundleName].AddRefence(assetName);
        //        dependencies = manifest.GetAllDependencies(bundleName);
        //        for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //        {
        //            bundleHolders[dependencies[i]].AddRefence(assetName);
        //        }
        //        return assetHolder;
        //    }

        //    //2 从bundleHolders获取资源
        //    AssetBundle bundle = null;
        //    BundleHolder bundleHolder = null;
        //    // 没有加载过bundle
        //    if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
        //    {
        //        bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
        //        //存bundleHolder
        //        bundleHolder = new BundleHolder(bundle);
        //        bundleHolder.AddRefence(assetName);
        //        bundleHolders.Add(bundleName, bundleHolder);
        //    }
        //    else
        //    {
        //        bundleHolder.AddRefence(assetName);
        //        bundle = bundleHolder.Get();
        //    }
            

        //    // 加载依赖的bundle
        //    dependencies = manifest.GetAllDependencies(bundleName);
        //    for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //    {
        //        BundleHolder dependBundleHolder = null;
        //        if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
        //        {
        //            //已经存在的bundle只增加引用
        //            dependBundleHolder.AddRefence(assetName);
        //            continue;
        //        }
        //        //没加载过的
        //        AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
        //        dependBundleHolder = new BundleHolder(dependBundle);
        //        dependBundleHolder.AddRefence(assetName);
        //        //存bundleHolder
        //        bundleHolders.Add(dependencies[i], dependBundleHolder);
        //    }

        //    //所有bundle加载完毕
        //    t = bundle.LoadAsset<T>(assetName);

        //    //新建AssetHolder
        //    assetHolder = new AssetHolder(t);
        //    //记录资源引用
        //    assetHolder.AddRefence(go);
        //    if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //    {
        //        assetHolders = new HashSet<AssetHolder>();
        //        goAssetHolders.Add(go, assetHolders);
        //    }
        //    assetHolders.Add(assetHolder);
        //    nameAssetHolders.Add(assetName, assetHolder);

        //    return assetHolder;
        //}

        //public GameObject LoadGameObject(string assetName)
        //{
        //    GameObject go = null;
        //    // 1 从nameAssetHolders获取资源
        //    AssetHolder assetHolder = null;
        //    GameObject prefab = null;
        //    string bundleName = GetBundleName(assetName);
        //    HashSet<AssetHolder> assetHolders = null;
        //    string[] dependencies = null;
        //    if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
        //    {
        //        prefab = assetHolder.Get<GameObject>();
        //        go = GameObject.Instantiate<GameObject>(prefab);
        //        //记录资源引用
        //        assetHolder.AddRefence(go);
        //        if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //        {
        //            assetHolders = new HashSet<AssetHolder>();
        //            goAssetHolders.Add(go, assetHolders);
        //        }
        //        assetHolders.Add(assetHolder);

        //        //记录Bundle引用
        //        //nameAssetHolders有,那么bundleHolders必有
        //        bundleHolders[bundleName].AddRefence(assetName);
        //        dependencies = manifest.GetAllDependencies(bundleName);
        //        for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //        {
        //            bundleHolders[dependencies[i]].AddRefence(assetName);
        //        }
        //        return go;
        //    }

        //    //2 从bundleHolders获取资源
        //    AssetBundle bundle = null;
        //    BundleHolder bundleHolder = null;
        //    // 没有加载过bundle
        //    if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
        //    {
        //        bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
        //        //存bundleHolder
        //        bundleHolder = new BundleHolder(bundle);
        //        bundleHolder.AddRefence(assetName);
        //        bundleHolders.Add(bundleName, bundleHolder);
        //    }
        //    else
        //    {
        //        bundleHolder.AddRefence(assetName);
        //        bundle = bundleHolder.Get();
        //    }

        //    // 加载依赖的bundle
        //    dependencies = manifest.GetAllDependencies(bundleName);
        //    for(int i = 0, iMax = dependencies.Length; i < iMax; ++i)
        //    {
        //        BundleHolder dependBundleHolder = null;
        //        if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
        //        {
        //            //已经存在的bundle只增加引用
        //            dependBundleHolder.AddRefence(assetName);
        //            continue;
        //        }
        //        //没加载过的
        //        AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
        //        dependBundleHolder = new BundleHolder(dependBundle);
        //        dependBundleHolder.AddRefence(assetName);
        //        //存bundleHolder
        //        bundleHolders.Add(dependencies[i], dependBundleHolder);
        //    }

        //    //所有bundle加载完毕
        //    prefab = bundle.LoadAsset<GameObject>(assetName);
        //    go = GameObject.Instantiate<GameObject>(prefab);

        //    //新建AssetHolder
        //    assetHolder = new AssetHolder(prefab);
        //    //记录资源引用
        //    assetHolder.AddRefence(go);
        //    if (!goAssetHolders.TryGetValue(go, out assetHolders))
        //    {
        //        assetHolders = new HashSet<AssetHolder>();
        //        goAssetHolders.Add(go, assetHolders);
        //    }
        //    assetHolders.Add(assetHolder);
        //    nameAssetHolders.Add(assetName, assetHolder);

        //    return go;
        //}



#endregion

#region 异步实验
        //delegate void LoadBundleCallback(AssetBundleRequest request);

        //void LoadAssetAsync()
        //{
        //    string bundleName = "MyCube-Parent";
        //    string assetName = "MyCube-Parent";

        //    string[] dependencies = manifest.GetAllDependencies(bundleName);

        //    StartCoroutine(
        //        ColoadBundle(assetName, bundleName, dependencies,
        //            (request) =>
        //            {
        //                GameObject prefab = request.asset as GameObject;
        //                GameObject go = GameObject.Instantiate<GameObject>(prefab);
        //                GameObject.DestroyImmediate(prefab, true);
        //            }));
        //}


        //IEnumerator ColoadBundle(string assetName, string currBundle, string[] dependentbundles, LoadBundleCallback callback)
        //{
        //    for (int i = 0; i < dependentbundles.Length; i++)
        //    {
        //        var dependentRequest = AssetBundle.LoadFromFileAsync(
        //            Path.Combine(Application.streamingAssetsPath, "Bundles/" + dependentbundles[i]));
        //        yield return dependentRequest;
        //    }

        //    var currRequest = AssetBundle.LoadFromFileAsync(
        //        Path.Combine(Application.streamingAssetsPath, "Bundles/" + currBundle));

        //    yield return currRequest;

        //    StartCoroutine(CoLoadAsset(currRequest.assetBundle, assetName, callback));

        //}

        //IEnumerator CoLoadAsset(AssetBundle bundle, string assetName, LoadBundleCallback callback)
        //{
        //    var assetRequest = bundle.LoadAssetAsync<GameObject>(assetName);
        //    yield return assetRequest;
        //    callback(assetRequest);

        //}

        //IEnumerator UnityLoadAssetAsync()
        //{
        //    var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "myassetBundle"));
        //    yield return bundleLoadRequest;

        //    var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        //    if (myLoadedAssetBundle == null)
        //    {
        //        Debug.Log("Failed to load AssetBundle!");
        //        yield break;
        //    }

        //    var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("MyObject");
        //    yield return assetLoadRequest;

        //    GameObject prefab = assetLoadRequest.asset as GameObject;
        //    Instantiate(prefab);

        //    myLoadedAssetBundle.Unload(false);
        //}
#endregion

    }
}

