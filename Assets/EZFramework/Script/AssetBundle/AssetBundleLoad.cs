using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EZFramework
{
    public class AssetBundleLoad : EZMonoBehaviour
    {

        public override void Start()
        {
            //StartCoroutine(Load());
        }

        private AssetBundle AssetBundle;
        private IEnumerator Load()
        {
            if (AssetBundle == null)
            {
                using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(Application.streamingAssetsPath + "/ui"))
                {
                    yield return uwr.SendWebRequest();
                    if (uwr.isNetworkError || uwr.isHttpError)
                    {
                        Debug.Log(uwr.error);
                    }
                    else
                    {
                        AssetBundle = DownloadHandlerAssetBundle.GetContent(uwr);

                    }
                }
            }
        }


    }
}