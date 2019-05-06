﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    /// <summary>
    /// 异步接口
    /// </summary>
    public partial class BundleLoader : IResourceLoader
    {
        public class BundleAsyncRequest
        {
            static int requestID;
            public int currRequestID; 
            public string assetName;
            public E_LoadAsset eLoadAsset;

            public BundleAsyncRequest(string assetName, E_LoadAsset eLoadAsset)
            {
                this.assetName = assetName;
                this.eLoadAsset = eLoadAsset;
                currRequestID = requestID++;
            }
        }

        Queue<BundleAsyncRequest> bundleAsyncs = new Queue<BundleAsyncRequest>();

        public  void LoadAssetAsync(string assetName, System.Action<AssetGetter> callback)
        {
            LoadAssetAsync<AssetGetter>(assetName, E_LoadAsset.LoadSingle, callback);
        }

        public  void LoadGameObjectAsync(string assetName, System.Action<GameObjectGetter> callback)
        {
            LoadAssetAsync<GameObjectGetter>(assetName, E_LoadAsset.LoadSingle, callback);
        }

        public  void LoadAllAssetsAsync(string assetName, System.Action<AssetGetter> callback)
        {
            LoadAssetAsync<AssetGetter>(assetName, E_LoadAsset.LoadAll, callback);
        }

        void LoadAssetAsync<T>(string assetName, E_LoadAsset eloadAsset, System.Action<T> callback)
            where T : IAssetGetter, new()
        {
            T getter;
            string bundleName = GetBundleName(assetName);
            if (LoadAssetFromNameAssetHolder(assetName, bundleName, out getter))
            {
                if (callback != null)
                {
                    callback(getter);
                }
                return;
            }

            BundleAsyncRequest bundleRequest = new BundleAsyncRequest(assetName, eloadAsset);
            Logger.LogWarp.Log(assetName + " request " + bundleRequest.currRequestID);
            bundleAsyncs.Enqueue(bundleRequest);
            RunCoroutine.Run(CoBundleAsyncRequest<T>(bundleRequest, callback));
        }

        IEnumerator CoBundleAsyncRequest<T>(BundleAsyncRequest bundleRequest, System.Action<T> callback)
            where T : IAssetGetter, new()
        {
            while (bundleRequest.currRequestID != bundleAsyncs.Peek().currRequestID)
            {
                Logger.LogWarp.Log(bundleRequest.currRequestID + " != " + bundleAsyncs.Peek().currRequestID + " waiting...");
                yield return null;
            }

            //等待完bundleRequest再次检查有没有被加载过。防止同一资源加载出错
            Logger.LogWarp.Log(bundleRequest.currRequestID + " wait finished!");
            string bundleName = GetBundleName(bundleRequest.assetName);
            T getter;
            if (LoadAssetFromNameAssetHolder(bundleRequest.assetName, bundleName, out getter))
            {
                if (callback != null)
                {
                    callback(getter);
                }
                bundleAsyncs.Dequeue();
                yield break;
            }

            yield return (CoLoadBundleAsync<T>(bundleRequest.assetName, bundleName, bundleRequest.eLoadAsset, callback));
        }

        IEnumerator CoLoadBundleAsync<T>(string assetName, string bundleName, E_LoadAsset eloadAsset, System.Action<T> callback)
            where T : IAssetGetter, new()
        {
            AssetBundle bundle = null;
            BundleHolder bundleHolder = null;
            AssetBundleCreateRequest bundleRequest;
            // 没有加载过bundle
            string bundlePath;
            if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
            {
                bundlePath = GetBundlePath(bundleName);
                //bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(bundleRootPath, bundleName));
                bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
                while (!bundleRequest.isDone)
                {
                    yield return bundleRequest;
                }

                //存bundleHolder
                bundle = bundleRequest.assetBundle;
                bundleHolder = new BundleHolder(bundle);
                bundleHolder.AddRefence(assetName);
                //Debug.LogError(Time.realtimeSinceStartup + " " + bundleName + " " + assetName);
                bundleHolders.Add(bundleName, bundleHolder);
            }
            else
            {
                bundleHolder.AddRefence(assetName);
                bundle = bundleHolder.Get();
            }

            // 加载依赖的bundle
            string[] dependencies = manifest.GetAllDependencies(bundleName);
            for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
            {
                BundleHolder dependBundleHolder = null;
                if (bundleHolders.TryGetValue(dependencies[i], out dependBundleHolder))
                {
                    //已经存在的bundle只增加引用
                    dependBundleHolder.AddRefence(assetName);
                    continue;
                }
                //没加载过的
                AssetBundle dependBundle = null;
                bundlePath = GetBundlePath(dependencies[i]);
                bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
                while(!bundleRequest.isDone)
                {
                    yield return bundleRequest;
                }
                dependBundle = bundleRequest.assetBundle;
                dependBundleHolder = new BundleHolder(dependBundle);
                dependBundleHolder.AddRefence(assetName);
                //存bundleHolder
                bundleHolders.Add(dependencies[i], dependBundleHolder);
            }

            yield return (CoLoadAssetAsync<T>(bundle, assetName, eloadAsset, callback));
        }

        IEnumerator CoLoadAssetAsync<T>(AssetBundle assetBundle, string assetName, E_LoadAsset eloadAsset, System.Action<T> callback)
            where T : IAssetGetter, new()
        {
            AssetBundleRequest assetRequest = null;
            switch (eloadAsset)
            {
                case E_LoadAsset.LoadSingle:
                    int index = assetName.LastIndexOf("/");
                    string assetNameInBundle = assetName.Substring(index + 1);
                    assetRequest = assetBundle.LoadAssetAsync(assetNameInBundle);
                    break;
                case E_LoadAsset.LoadAll:
                    assetRequest = assetBundle.LoadAllAssetsAsync<Object>();
                    break;
            }

            while (!assetRequest.isDone)
            {
                yield return assetRequest;
            }

            AssetHolder assetHolder = null;
            switch (eloadAsset)
            {
                case E_LoadAsset.LoadSingle:
                    assetHolder = new AssetHolder(assetRequest.asset);
                    break;
                case E_LoadAsset.LoadAll:
                    assetHolder = new AssetHolder(assetRequest.allAssets);
                    break;
            }

            T getter = new T();
            getter.SetAssetHolder(assetHolder);
            nameAssetHolders.Add(assetName, assetHolder);
#if UNITY_EDITOR
            var request = bundleAsyncs.Dequeue();
            Logger.LogWarp.Log(request.assetName + " " + request.currRequestID + " Dequeue");
#else
            bundleAsyncs.Dequeue();
#endif
            if (callback != null)
            {
                callback(getter);
            }
            
        }

    }
}
