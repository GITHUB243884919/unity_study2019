using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
using UnityEngine.SceneManagement;

namespace UFrame.ResourceManagement
{
    public class SceneManagement : Singleton<SceneManagement>, ISingleton
    {
        bool loadFinished = false;
        public void Init()
        {
            loadFinished = false;
        }

        public void LoadScene(string scenePath, System.Action callback)
        {
            loadFinished = false;
            ResHelper.LoadScene(scenePath);
            SceneManager.sceneLoaded += (a, b) =>
            {
                loadFinished = true;
            };

            RunCoroutine.Run(CoLoadFinished(callback));
        }

        IEnumerator CoLoadFinished(System.Action callback)
        {
            if (!loadFinished)
            {
                yield return null;
            }

            if (callback != null)
            {
                callback();
            }
        }
    }
}

