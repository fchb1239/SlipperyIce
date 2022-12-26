using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace SlipperyIce.Behaviours
{
    public class IceHandler : MonoBehaviour
    {
        public bool isModded;

        internal Texture iceTex;
        internal Texture defaultTex;

        void Awake()
        {
            defaultTex = GetComponent<Renderer>().material.mainTexture;
            StartCoroutine(LoadImage());
        }

        public void Enable()
        {
            if (isModded) SetGroundTexture(true);
        }

        public void Disable() => SetGroundTexture(false);

        public void JoinedModded()
        {
            isModded = true;
            if (modEnabled) SetGroundTexture(true);
        }

        public void LeftModded()
        {
            isModded = false;
            SetGroundTexture(false);
        }

        void SetGroundTexture(bool isIce)
        {
            if (mat && !(enabled && isModded)) return;

            if (mat)
            {
                Material[] materials = transform.gameObject.GetComponent<MeshRenderer>().materials;
                materials[0] = ice;
                transform.gameObject.GetComponent<MeshRenderer>().materials = materials;
                return;
            }

            Material[] materials = transform.gameObject.GetComponent<MeshRenderer>().materials;
            materials[0] = ground;
            transform.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }

        private IEnumerator LoadImage()
        {
            var imageGet = GetImageRequest();
            yield return imageGet.SendWebRequest();

            Texture2D tex = new Texture2D(2048, 2048, TextureFormat.RGB24, false);
            tex.filterMode = FilterMode.Point;
            tex.LoadImage(imageGet.downloadHandler.data);
        }

        private UnityEngine.Networking.UnityWebRequest GetImageRequest()
        {
            var request = new UnityEngine.Networking.UnityWebRequest($"https://raw.githubusercontent.com/fchb1239/SlipperyIce/main/forestatlasButIcy.png", "GET");
            request.downloadHandler = new DownloadHandlerBuffer();

            return request;
        }
    }
}
