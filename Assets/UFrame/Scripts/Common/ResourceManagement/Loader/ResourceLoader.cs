using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{    
    public abstract class ResourceLoader
    {
        public abstract void Init();
        public abstract AssetGetter LoadAsset(string assetPath);

        public abstract GameObjectGetter LoadGameObject(string assetPath);

        public abstract AssetGetter LoadAllAssets(string assetPath);

        public abstract void LoadAssetAsync(string assetPath, System.Action<AssetGetter> callback);

        public abstract void LoadGameObjectAsync(string assetPath, System.Action<GameObjectGetter> callback);

        public abstract void LoadAllAssetsAsync(string assetPath, System.Action<AssetGetter> callback);

        public abstract void RealseAllUnUse();
        public abstract void DestroyGameObject(GameObject go);
        public abstract void RealseAsset(AssetHolder assetHolder, GameObject go);

        public abstract void RealseAsset(GameObject go);

        public abstract void AddGameObjectAssetHolder(GameObject go, AssetHolder assetHolder);
    }
}

