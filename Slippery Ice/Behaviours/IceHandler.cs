using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SlipperyIce.Behaviours
{
    public class IceHandler : MonoBehaviour
    {
        public bool isModded;

        internal Texture iceTex;
        internal Texture defaultTex;

        internal void Awake()
        {
            defaultTex = GetComponent<Renderer>().material.mainTexture;
            StartCoroutine(routine: LoadImage());
        }

        public void Enable() => SetGroundTexture(true);

        public void Disable() => SetGroundTexture(false);

        public void JoinedModded()
        {
            isModded = true;
            SetGroundTexture(true);
        }

        public void LeftModded()
        {
            isModded = false;
            SetGroundTexture(false);
        }

        internal void SetGroundTexture(bool isIce)
        {
            // if (isIce && !(Plugin.Instance.enabled && isModded)) return;
            GetComponent<Renderer>().material.mainTexture = isIce ? iceTex as Texture2D : defaultTex as Texture2D;
        }

        internal IEnumerator LoadImage()
        {
            var imageGet = GetImageRequest();
            yield return imageGet.SendWebRequest();

            Texture2D tex = new Texture2D(defaultTex.width, defaultTex.height, TextureFormat.RGBA32, false);
            tex.LoadImage(imageGet.downloadHandler.data);
            tex.Apply();

            tex.filterMode = defaultTex.filterMode;
            tex.name = "iceground";

            iceTex = tex;
        }

        internal UnityWebRequest GetImageRequest()
        {
            var uImageRequest = new UnityWebRequest("https://raw.githubusercontent.com/developer9998/SlipperyIce/main/iceground.png", "GET");
            uImageRequest.downloadHandler = new DownloadHandlerBuffer();
            return uImageRequest;
        }
    }
}
