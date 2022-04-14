using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SlipperyIce.Behaviours
{
    public class IceHandler : MonoBehaviour
    {
        public static IceHandler instance;
        private Material ice;
        private Material ground;
        public bool modEnabled = false;
        public bool isModded = false;

        void Awake()
        {
            instance = this;
            ice = new Material(Shader.Find("Standard"));
            StartCoroutine(LoadImage());
            ground = gameObject.GetComponent<MeshRenderer>().materials[0];
        }

        public void Enable()
        {
            modEnabled = true;
            if (isModded)
                SetGroundTexture(true);
        }

        public void Disable()
        {
            modEnabled = false;
            SetGroundTexture(false);
        }

        public void JoinedModded()
        {
            isModded = true;
            if(modEnabled)
                SetGroundTexture(true);
        }

        public void LeftModded()
        {
            isModded = false;
            SetGroundTexture(false);
        }

        void SetGroundTexture(bool mat)
        {
            //Lemming why you make everything an atlas :(
            if (mat)
            {
                Material[] materials = transform.gameObject.GetComponent<MeshRenderer>().materials;
                materials[0] = ice;
                transform.gameObject.GetComponent<MeshRenderer>().materials = materials;
            }
            else
            {
                Material[] materials = transform.gameObject.GetComponent<MeshRenderer>().materials;
                materials[0] = ground;
                transform.gameObject.GetComponent<MeshRenderer>().materials = materials;
            }
        }

        //https://github.com/fchb1239/UnityImageDownloader
        private IEnumerator LoadImage()
        {
            //Sends web request
            Debug.Log("Downloading image");
            var imageGet = GetImageRequest();
            yield return imageGet.SendWebRequest();

            //Creates and loads texture
            Debug.Log("Creating and loading texture");
            Texture2D tex = new Texture2D(2048, 2048, TextureFormat.RGB24, false);
            tex.filterMode = FilterMode.Point;
            tex.LoadImage(imageGet.downloadHandler.data);

            //Applies to material
            Debug.Log("Applying to material");
            ice.mainTexture = tex;

            Debug.Log("Done");
        }

        private UnityEngine.Networking.UnityWebRequest GetImageRequest()
        {
            var request = new UnityEngine.Networking.UnityWebRequest($"https://raw.githubusercontent.com/fchb1239/SlipperyIce/main/forestatlasButIcy.png", "GET");

            request.downloadHandler = new DownloadHandlerBuffer();

            return request;
        }
    }
}
