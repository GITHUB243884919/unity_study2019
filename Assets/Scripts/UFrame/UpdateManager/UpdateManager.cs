using System.Collections;
using System.Collections.Generic;
using UFrame.Common;
using UnityEngine;
using System.IO;
/// <summary>
/// todo
/// 1.unitylogo 没有扩展名
/// 2.下载不应该断点续传，因为存在同名旧文件
/// 3.更新执行前应该有界面，有界面就会用资源管理器，如果更新了bundle配置文件，那么资源管理需要重新加载bundle配置文件
/// 4.打包脚本按window那个写
/// 5.从包内读取AB文件是携程，写完成拷贝逻辑
/// </summary>
namespace UFrame.Update
{
    public class UpdateManager : Singleton<UpdateManager>, ISingleton
    {
        string innerGameVersion;
        string outterGameVersion;
        string innerBundleRootPath;
        string outerBundleRootPath;
        string serverDownloadUrl;
        string localDownLoadRoot;

        string versionFileName;
        string versionFileNameTmp;

        string assetbundleFileName;
        string assetbundleFileNameTmp;

        string bundleHashFileName;
        string bundleHashFileNameTmp;

        string manifestFileName;
        string manifestFileNameTmp;

        bool couldComparerVersion = false;
        bool coundComparerDetail = false;
        public void Init()
        {
            string ApplicationStreamingPath = Application.streamingAssetsPath;
            innerBundleRootPath = Path.Combine(ApplicationStreamingPath, UFrameConst.Bundle_Root_Dir);
            outerBundleRootPath = Path.Combine(Application.persistentDataPath, UFrameConst.Bundle_Root_Dir);

            serverDownloadUrl = @"http://127.0.0.1:8080/";

            localDownLoadRoot = Path.Combine(Application.persistentDataPath, UFrameConst.Bundle_Root_Dir);
            versionFileName = UFrameConst.Game_Version_Txt_Name + UFrameConst.Bundle_Extension;
            versionFileNameTmp = versionFileName + UFrameConst.Download_Extension;

            assetbundleFileName = Path.GetFileNameWithoutExtension(UFrameConst.Asset_Bundle_Txt_Name) + UFrameConst.Bundle_Extension;
            assetbundleFileNameTmp = assetbundleFileName + UFrameConst.Download_Extension;

            bundleHashFileName = Path.GetFileNameWithoutExtension(UFrameConst.Bundle_Hash_Txt_Name) + UFrameConst.Bundle_Extension;
            bundleHashFileNameTmp = bundleHashFileName + UFrameConst.Download_Extension;

            manifestFileName = UFrameConst.Bundle_Root_Dir + UFrameConst.Bundle_Extension;
            manifestFileNameTmp = manifestFileName + UFrameConst.Download_Extension;

            couldComparerVersion = false;
            coundComparerDetail = false;
        }

        //public static string GetInnerGameVersion()
        //{
        //    var versionAsset = Resources.Load<TextAsset>(UFrameConst.Game_Version_Txt_Name);

        //    return versionAsset.text;
        //}

        //public static string GetOutterGameVersion()
        //{
        //    string path = Path.Combine(Application.persistentDataPath, UFrameConst.Bundle_Root_Dir);

        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //        return "";
        //    }

        //    path = Path.Combine(path, UFrameConst.Game_Version_Txt_Name);
        //    path += ".txt";
        //    Logger.LogWarp.Log(path);

        //    if (!File.Exists(path))
        //    {
        //        return "";
        //    }

        //    Logger.LogWarp.Log(path);

        //    return File.ReadAllText(path);
        //}

        #region 版本号
        string GetGameVersion(string rootPath)
        {
            string result = "";
            string bundlePath = UFrameConst.Game_Version_Txt_Name;
            bundlePath += UFrameConst.Bundle_Extension;
            bundlePath = Path.Combine(rootPath, bundlePath);
            if (!File.Exists(bundlePath))
            {
                return result;
            }
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var txt = bundle.LoadAsset<TextAsset>(UFrameConst.Game_Version_Txt_Name);
            result = txt.text;

            bundle.Unload(true);

            return result;
        }

        string GetGameVersion2(string bundlePath)
        {
            string result = "";
            if (!File.Exists(bundlePath))
            {
                Logger.LogWarp.LogError("NOT Exists [" + bundlePath + "]");
                return result;
            }
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var txt = bundle.LoadAsset<TextAsset>(UFrameConst.Game_Version_Txt_Name);
            result = txt.text;

            bundle.Unload(true);

            return result;
        }

        string GetTxtFromBundle(string bundlePath, string bundleName)
        {
            string result = "";
            if (!File.Exists(bundlePath))
            {
                return result;
            }
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var txt = bundle.LoadAsset<TextAsset>(bundleName);
            result = txt.text;

            bundle.Unload(true);

            return result;
        }

        public string GetInnerGameVersion()
        {
            return GetGameVersion(innerBundleRootPath);
        }

        public string GetOutterGameVersion()
        {
            return GetGameVersion(outerBundleRootPath);
        }

        /// <summary>
        /// 比较x.x格式的版本号
        /// 1  a > b
        /// 0  a == b
        /// -1 a < b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ComparerVersion(string a, string b)
        {
            string[] aSplit = a.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
            string[] bSplit = b.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);

            if (System.Convert.ToInt32(aSplit[0]) > System.Convert.ToInt32(bSplit[0]))
            {
                return 1;
            }
            else if (System.Convert.ToInt32(aSplit[0]) == System.Convert.ToInt32(bSplit[0]))
            {
                if (System.Convert.ToInt32(aSplit[1]) > System.Convert.ToInt32(bSplit[1]))
                {
                    return 1;
                }
                else if (System.Convert.ToInt32(aSplit[1]) == System.Convert.ToInt32(bSplit[1]))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }
        #endregion

        #region 拷贝AB
        public void CopyAssetBundle(string source, string dest, System.Action callback = null)
        {
            Logger.LogWarp.Log("CopyAssetBundle " + source + " " + dest);
            RunCoroutine.Run(CoCopyAssetBundle(source, dest, callback));
        }

        IEnumerator CoCopyAssetBundle(string source, string dest, System.Action callback)
        {
            using (WWW www = new WWW(source))
            {
                yield return www;
                if (www.error == null && www.isDone)
                {
                    File.WriteAllBytes(dest, www.bytes);
                    if (callback != null)
                    {
                        callback();
                    }
                }
            }
        }
        #endregion

        #region 流程

        /// <summary>
        /// 确定本地版本信息，确保沙盒目录内版本相关信息文件正确
        /// </summary>
        public void EnsureLocalGameVersionInfomation()
        {
            string innerGameVersion = GetInnerGameVersion();
            Logger.LogWarp.Log("innerGameVersion" + innerGameVersion);
            string outterGameVersion = GetOutterGameVersion();
            Logger.LogWarp.Log("outterGameVersion" + outterGameVersion);

            //外部版本号为空（首次安装包），或者内部版本号> 外部版本号（大版本更新）
            if (string.IsNullOrEmpty(outterGameVersion) || UFrame.Update.UpdateManager.ComparerVersion(innerGameVersion, outterGameVersion) >= 0)
            {
                //Copy bundle 配置文件到沙盒
                Logger.LogWarp.Log("Copy version and AB's config to outter dir");

                string innerRootPath = Path.Combine(Application.streamingAssetsPath, UFrameConst.Bundle_Root_Dir);
                string outterRootPath = Path.Combine(Application.persistentDataPath, UFrameConst.Bundle_Root_Dir);

                if (!Directory.Exists(outterRootPath))
                {
                    Directory.CreateDirectory(outterRootPath);
                }

                //copy version
                string innerVersionPath = Path.Combine(innerRootPath, UFrameConst.Game_Version_Txt_Name);
                innerVersionPath += UFrameConst.Bundle_Extension;
                string outterVersionPath = Path.Combine(outterRootPath, UFrameConst.Game_Version_Txt_Name);
                outterVersionPath += UFrameConst.Bundle_Extension;
                CopyAssetBundle(innerVersionPath, outterVersionPath);

                //copy asset-bundle
                string innerAssetBundlePath = Path.Combine(innerRootPath, Path.GetFileNameWithoutExtension(UFrameConst.Asset_Bundle_Txt_Name));
                innerAssetBundlePath += UFrameConst.Bundle_Extension;
                string outterAssetBundlePath = Path.Combine(outterRootPath, Path.GetFileNameWithoutExtension(UFrameConst.Asset_Bundle_Txt_Name));
                outterAssetBundlePath += UFrameConst.Bundle_Extension;
                CopyAssetBundle(innerAssetBundlePath, outterAssetBundlePath);

                //copy manifest
                string innerManifestPath = Path.Combine(innerRootPath, UFrameConst.Bundle_Root_Dir);
                innerManifestPath += UFrameConst.Bundle_Extension;
                string outterManifestPath = Path.Combine(outterRootPath, UFrameConst.Bundle_Root_Dir);
                outterManifestPath += UFrameConst.Bundle_Extension;
                CopyAssetBundle(innerManifestPath, outterManifestPath);

                //copy bundle hash
                string innerBundleHashPath = Path.Combine(innerRootPath, Path.GetFileNameWithoutExtension(UFrameConst.Bundle_Hash_Txt_Name));
                innerBundleHashPath += UFrameConst.Bundle_Extension;
                string outterBundleHashPath = Path.Combine(outterRootPath, Path.GetFileNameWithoutExtension(UFrameConst.Bundle_Hash_Txt_Name));
                outterBundleHashPath += UFrameConst.Bundle_Extension;
                CopyAssetBundle(innerBundleHashPath, outterBundleHashPath);
            }
        }

        public void DownLoadServerVersionInfomation()
        {
            //var http = new HttpDownLoad();
            //string URL = @"http://127.0.0.1:8080/a.txt";
            //http.DownLoad(URL, Application.streamingAssetsPath, "a.txt", DownLoadCallback);



            var http = new HttpDownLoad();
            http.DownLoad(this.serverDownloadUrl + versionFileName, localDownLoadRoot, versionFileNameTmp, DownloadVersionCallback);
        }

        
        void DownloadVersionCallback()
        {
            couldComparerVersion = true;
        }

        void ComparerVersion()
        {
            if (!couldComparerVersion)
            {
                return;
            }
            couldComparerVersion = false;
            Logger.LogWarp.Log("ComparerVersion");
            Logger.LogWarp.Log(Path.Combine(localDownLoadRoot, versionFileName));

            string localVersion = GetGameVersion2(Path.Combine(localDownLoadRoot, versionFileName));
            string serverVersion = GetGameVersion2(Path.Combine(localDownLoadRoot, versionFileNameTmp));
            Logger.LogWarp.Log("localVersion=" + localVersion + " serverVersion=" + serverVersion);
            int retCode = ComparerVersion(serverVersion, localVersion);
            //服务器版本大
            if (retCode >= 0)
            {
                var http = new HttpDownLoad();
                Dictionary<string, string> urls = new Dictionary<string, string>();
                urls.Add(this.serverDownloadUrl + this.assetbundleFileName, assetbundleFileNameTmp);
                urls.Add(this.serverDownloadUrl + this.bundleHashFileName, bundleHashFileNameTmp);
                urls.Add(this.serverDownloadUrl + this.manifestFileName, manifestFileNameTmp);
                http.DownLoads(urls, localDownLoadRoot, DownLoadDetailCallback);
            }
        }

        void DownLoadDetailCallback()
        {
            this.coundComparerDetail = true;
        }

        void ComparerDetail()
        {
            if (!coundComparerDetail)
            {
                return;
            }
            coundComparerDetail = false;
            Logger.LogWarp.Log("ComparerDetail");

            string localBundlePath = Path.Combine(localDownLoadRoot, this.bundleHashFileName);
            string serverBundlePath = Path.Combine(localDownLoadRoot, this.bundleHashFileNameTmp);
            string bundleName = Path.GetFileNameWithoutExtension(UFrameConst.Bundle_Hash_Txt_Name);

            string localBundleHash = GetTxtFromBundle(localBundlePath, bundleName);
            string serverBundleHash = GetTxtFromBundle(serverBundlePath, bundleName);
            var localHashSet = AddTxtToHashSet(localBundleHash);
            var serverHashSet = AddTxtToHashSet(serverBundleHash);

            //localHashSet.ExceptWith(serverHashSet);
            //serverHashSet.Add("111112222");
            //serverHashSet.ExceptWith(localHashSet);
            
            Logger.LogWarp.Log("serverHashSet " + serverHashSet.Count);

            Dictionary<string, string> urls = new Dictionary<string, string>();
            foreach(var v in serverHashSet)
            {
                string[] strs = v.Split(new char[] {'='}, System.StringSplitOptions.RemoveEmptyEntries);
                urls.Add(this.serverDownloadUrl + strs[0], strs[0]);
            }

            var http = new HttpDownLoad();
            http.DownLoads(urls, localDownLoadRoot, DownloadBundleCallback);
        }

        void DownloadBundleCallback()
        {
            Logger.LogWarp.Log("all bundle download!");
        }

        HashSet<string> AddTxtToHashSet(string txt)
        {
            HashSet<string> hash = new HashSet<string>();
            string[] strs = txt.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strs.Length; i++)
            {
                hash.Add(strs[i]);
            }

            return hash;
        }
        #endregion

        public void Tick()
        {
            ComparerVersion();
            ComparerDetail();
        }

    }
}

