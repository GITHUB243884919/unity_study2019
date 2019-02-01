using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{

    public class GameObjectGetter : IAssetGetter
    {
        AssetHolder assetHolder;
        public void SetAssetHolder(AssetHolder assetHolder)
        {
            this.assetHolder = assetHolder;
        }

        public GameObject Get()
        {
            GameObject prefab = assetHolder.Get<GameObject>();
            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            //记录资源引用
            assetHolder.AddRefence(go);
            BundleLoader.GetInstance().AddGameObjectAssetHolder(go, assetHolder);
            return go;
        }

        public void Release(GameObject go)
        {
            BundleLoader.GetInstance().DestroyGameObject(go);
            assetHolder = null;
        }

        //public void AddRefence(GameObject refence)
        //{
        //    assetHolder.AddRefence(refence);
        //}

        //public void RemoveRefence(GameObject refence)
        //{
        //    assetHolder.AddRefence(refence);
        //}
    }

}
