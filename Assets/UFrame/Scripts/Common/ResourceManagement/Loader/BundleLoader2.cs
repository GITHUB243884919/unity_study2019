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
    /// LoadAll的限制，只能LoadAll资源类型，以整体的形式计数，公共Gameobject做为持有者
    /// 
    /// 同一个目录下资源不能同名，开发环境是用AseetDatabase，底层API需要扩展名。
    /// 所有的资源名称只能小写，用下划线分隔，安卓和ios大小写敏感，容易在PC上没事，换到移动平台出问题
    /// 
    /// GetAll仅仅用于非GameObject类型资源
    /// </summary>
    public partial class BundleLoader : MonoBehaviour
    {
        public enum E_LoadAsset
        {
            LoadSingle,
            LoadAll,
        }

        public AssetGetter LoadAllAssets(string assetName)
        {
            return LoadAsset<AssetGetter>(assetName, E_LoadAsset.LoadAll);
        }

        public GameObjectGetter LoadGameObject(string assetName)
        {
            return LoadAsset<GameObjectGetter>(assetName, E_LoadAsset.LoadSingle);
        }

        public AssetGetter LoadAsset(string assetName)
        {
            return LoadAsset<AssetGetter>(assetName, E_LoadAsset.LoadSingle);
        }

        T LoadAsset<T>(string assetName, E_LoadAsset eLoadAsset)
            where T : IAssetGetter, new()
        {
            T getter;
            string bundleName = GetBundleName(assetName);
            if (LoadAssetFromNameAssetHolder(assetName, bundleName, out getter))
            {
                return getter;
            }

            AssetBundle bundle = LoadBundle(assetName, bundleName);
            if (bundle == null)
            {
                return getter;
            }

            //所有bundle加载完毕
            AssetHolder assetHolder = null;
            switch (eLoadAsset)
            {
                case E_LoadAsset.LoadSingle:
                    int index = assetName.LastIndexOf("/");
                    string assetNameInBundle = assetName.Substring(index + 1);
                    Debug.LogError(assetName + " " + assetNameInBundle);

                    //assetHolder = new AssetHolder(bundle.LoadAsset(assetName));
                    assetHolder = new AssetHolder(bundle.LoadAsset(assetNameInBundle));
                    break;
                case E_LoadAsset.LoadAll:
                    assetHolder = new AssetHolder(bundle.LoadAllAssets());
                    break;
            }
            //新建AssetHolder
            //AssetHolder assetHolder = new AssetHolder(obj);
            getter.SetAssetHolder(assetHolder);

            nameAssetHolders.Add(assetName, assetHolder);

            return getter;
        }

        bool LoadAssetFromNameAssetHolder<T>(string assetName, string bundleName, out T getter)
            where T : IAssetGetter, new()
        {
            // 1 从nameAssetHolders获取资源
            AssetHolder assetHolder = null;
            bundleName = GetBundleName(assetName);
            string[] dependencies = null;
            getter = new T();
            if (nameAssetHolders.TryGetValue(assetName, out assetHolder))
            {
                getter.SetAssetHolder(assetHolder);

                //记录Bundle引用
                //nameAssetHolders有,那么bundleHolders必有
                bundleHolders[bundleName].AddRefence(assetName);
                dependencies = manifest.GetAllDependencies(bundleName);
                for (int i = 0, iMax = dependencies.Length; i < iMax; ++i)
                {
                    bundleHolders[dependencies[i]].AddRefence(assetName);
                }
                return true;
            }

            return false;
        }

        AssetBundle LoadBundle(string assetName, string bundleName)
        {
            AssetBundle bundle = null;
            BundleHolder bundleHolder = null;
            // 没有加载过bundle
            string bundlePath = GetBundlePath(bundleName);
            if (!bundleHolders.TryGetValue(bundleName, out bundleHolder))
            {
                //bundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, bundleName));
                bundle = AssetBundle.LoadFromFile(bundlePath);
                //存bundleHolder
                bundleHolder = new BundleHolder(bundle);
                bundleHolder.AddRefence(assetName);
                bundleHolders.Add(bundleName, bundleHolder);
            }
            else
            {
                bundleHolder.AddRefence(assetName);
                bundle = bundleHolder.Get();
            }

            // 加载依赖的bundle
            string [] dependencies = manifest.GetAllDependencies(bundleName);
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
                bundlePath = GetBundlePath(dependencies[i]);
                //AssetBundle dependBundle = AssetBundle.LoadFromFile(Path.Combine(bundleRootPath, dependencies[i]));
                AssetBundle dependBundle = AssetBundle.LoadFromFile(bundlePath);
                dependBundleHolder = new BundleHolder(dependBundle);
                dependBundleHolder.AddRefence(assetName);
                //存bundleHolder
                bundleHolders.Add(dependencies[i], dependBundleHolder);
            }

            return bundle;
        }

    }


}
