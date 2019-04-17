using System.Collections;
using System.Collections.Generic;
using UFrame.Common;
using UnityEngine;
using System.IO;

namespace UFrame.Update
{
    public class UpdateManager : Singleton<UpdateManager>, ISingleton
    {
        public void Init()
        {

        }

        public static string GetInnerGameVersion()
        {
            var versionAsset = Resources.Load<TextAsset>(UFrameConst.Game_Version_Txt_Name);

            return versionAsset.text;
        }

        public static string GetOutterGameVersion()
        {
            string path = Path.Combine(Application.persistentDataPath, UFrameConst.Bundle_Root_Dir);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return "";
            }

            path = Path.Combine(path, UFrameConst.Game_Version_Txt_Name);
            path += ".txt";
            Logger.LogWarp.Log(path);

            if (!File.Exists(path))
            {
                return "";
            }

            Logger.LogWarp.Log(path);

            return File.ReadAllText(path);
        }

        string innerGameVersion;
        string outterGameVersion;
        public void EnsureGameVersion()
        {
            innerGameVersion = ResHelper.GetInnerGameVersion();
            Logger.LogWarp.Log("innerGameVersion" + innerGameVersion);
            outterGameVersion = ResHelper.GetOutterGameVersion();
            Logger.LogWarp.Log("outterGameVersion" + outterGameVersion);

            //外部版本号为空（首次安装包），或者内部版本号> 外部版本号（大版本更新）
            if (string.IsNullOrEmpty(outterGameVersion) || ComparerVersion(innerGameVersion, outterGameVersion) >= 0)
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
            }
        }


        public void CopyAssetBundle(string source, string dest)
        {
            Logger.LogWarp.Log("CopyAssetBundle " + source + " " + dest);
            RunCoroutine.Run(CoCopyAssetBundle(source, dest));
        }
        IEnumerator CoCopyAssetBundle(string source, string dest)
        {
            using (WWW www = new WWW(source))
            {
                yield return www;
                if (www.error == null && www.isDone)
                {
                    File.WriteAllBytes(dest, www.bytes);
                }
            }
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
            string [] aSplit = a.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
            string [] bSplit = b.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);

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
    }
}

