using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    public class ResourceManager : MonoBehaviour
    {
        static ResourceManager instance;
        
        ResourceLoader resLoader;

        public static ResourceManager GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
#if UNITY_EDITOR
#if RES_AB
            //Editor模式下希望Bundle加载
            resLoader = new BundleLoader();
#else
            //Editor模式下希望Resources加载
            resLoader = new AssetDatabaseLoader();
#endif
#else
            //非Editor模式下
            //用Bundle加载
            resLoader = new BundleLoader();
#endif
            resLoader.Init();

        }

        public AssetGetter LoadAsset(string assetPath)
        {
            return resLoader.LoadAsset(assetPath);
        }

        public GameObjectGetter LoadGameObject(string assetPath)
        {
            return resLoader.LoadGameObject(assetPath);
        }

        public AssetGetter LoadAllAssets(string assetPath)
        {
            return resLoader.LoadAllAssets(assetPath);
        }

        public void LoadAssetAsync(string assetPath, System.Action<AssetGetter> callback)
        {
            resLoader.LoadAssetAsync(assetPath, callback);
        }

        public void LoadGameObjectAsync(string assetPath, System.Action<GameObjectGetter> callback)
        {
            resLoader.LoadGameObjectAsync(assetPath, callback);
        }
        public void LoadAllAssetsAsync(string assetPath, System.Action<AssetGetter> callback)
        {
            resLoader.LoadAllAssetsAsync(assetPath, callback);
        }

        public void RealseAllUnUse()
        {
            resLoader.RealseAllUnUse();
        }

        public void DestroyGameObject(GameObject go)
        {
            resLoader.DestroyGameObject(go);
        }

        public void RealseAsset(AssetHolder assetHolder, GameObject go)
        {
            resLoader.RealseAsset(assetHolder, go);
        }

        public void RealseAsset(GameObject go)
        {
            resLoader.RealseAsset(go);

        }
        public void AddGameObjectAssetHolder(GameObject go, AssetHolder assetHolder)
        {
            resLoader.AddGameObjectAssetHolder(go, assetHolder);
        }

    }
}


