#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UFrame.ResourceManagement
{
    public partial class AssetDatabaseLoader : ResourceLoader
    {
        /// <summary>
        /// 不带扩展名-带扩展名
        /// </summary>
        Dictionary<string, string> pathMaps = new Dictionary<string, string>();
        //static AssetDatabaseLoader instance;
        //public static AssetDatabaseLoader GetInstance()
        //{
        //    return instance;
        //}


        public override void Init()
        {
            //instance = this;
            LoadFileMap();
        }

        void LoadFileMap()
        {
            pathMaps.Clear();

            string[] filePaths = AssetDatabase.GetAllAssetPaths();
            foreach (var filePath in filePaths)
            {
                if (filePath.Contains("GameResources"))
                {
                    string unixFilePath = filePath.Replace('\\', '/');
                    string fileFindPath = unixFilePath.Replace("Assets/GameResources/", "");
                    int lastSplitIndex = fileFindPath.LastIndexOf('.');
                    //没有扩展名的算目录
                    if (lastSplitIndex <= 0)
                    {
                        continue;
                    }
                    fileFindPath = fileFindPath.Substring(0, lastSplitIndex);
                    fileFindPath = fileFindPath.ToLower();
                    if (pathMaps.ContainsKey(fileFindPath))
                    {
#if DEBUG && !PROFILER
                        Debug.LogErrorFormat("文件{0}的文件夹有同名文件 有可能导致加载错误 请处理!!!", fileFindPath);
#endif

                    }
                    pathMaps[fileFindPath] = unixFilePath;
                }
            }
        }

        string GetAssetPathWithExtend(string assetPath)
        {
            string result = null;
            pathMaps.TryGetValue(assetPath, out result);
            return result;
        }


        //同步

        public override AssetGetter LoadAsset(string assetPath)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(loadPath);
            AssetHolder assetHolder = new AssetHolder(obj);
            AssetGetter getter = new AssetGetter();
            getter.SetAssetHolder(assetHolder);
            return getter;
        }

        public override AssetGetter LoadAllAssets(string assetPath)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            Object [] objs = AssetDatabase.LoadAllAssetsAtPath(loadPath);
            AssetHolder assetHolder = new AssetHolder(objs);
            AssetGetter getter = new AssetGetter();
            getter.SetAssetHolder(assetHolder);
            return getter;
        }

        public override GameObjectGetter LoadGameObject(string assetPath)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(loadPath);

            AssetHolder assetHolder = new AssetHolder(prefab);
            GameObjectGetter getter = new GameObjectGetter();
            getter.SetAssetHolder(assetHolder);
            return getter;
        }

        //异步

        public override void LoadAssetAsync(string assetPath, System.Action<AssetGetter> callback)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(loadPath);
            AssetHolder assetHolder = new AssetHolder(obj);
            AssetGetter getter = new AssetGetter();
            getter.SetAssetHolder(assetHolder);
            callback(getter);
        }

        public override void LoadAllAssetsAsync(string assetPath, System.Action<AssetGetter> callback)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(loadPath);
            AssetHolder assetHolder = new AssetHolder(objs);
            AssetGetter getter = new AssetGetter();
            getter.SetAssetHolder(assetHolder);
            callback(getter);
        }

        public override void LoadGameObjectAsync(string assetPath, System.Action<GameObjectGetter> callback)
        {
            string loadPath = GetAssetPathWithExtend(assetPath.ToLower());
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(loadPath);

            AssetHolder assetHolder = new AssetHolder(prefab);
            GameObjectGetter getter = new GameObjectGetter();
            getter.SetAssetHolder(assetHolder);
            callback(getter);
        }

        public override void RealseAllUnUse()
        {

        }

        public override void DestroyGameObject(GameObject go)
        {

        }

        public override void RealseAsset(AssetHolder assetHolder, GameObject go)
        {

        }





    }
}
#endif
