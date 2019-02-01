using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.ResourceManagement
{

    public class AssetGetter: IAssetGetter
        //where T : class
    {
        AssetHolder assetHolder;
        public void SetAssetHolder(AssetHolder assetHolder)
        {
            this.assetHolder = assetHolder;
        }

        public T Get<T>(GameObject go) where T : Object 
        {
            T t = assetHolder.Get<T>();

            //记录资源引用
            assetHolder.AddRefence(go);
            BundleLoader.GetInstance().AddGameObjectAssetHolder(go, assetHolder);
            return t;
        }

        public object GetAll(GameObject go)
        {
            object obj = assetHolder.GetAll();
            assetHolder.AddRefence(go);
            BundleLoader.GetInstance().AddGameObjectAssetHolder(go, assetHolder);
            return obj;
        }

        public void Release(GameObject go)
        {
            BundleLoader.GetInstance().RealseAsset(assetHolder, go);
            assetHolder = null;
        }

    }

}
