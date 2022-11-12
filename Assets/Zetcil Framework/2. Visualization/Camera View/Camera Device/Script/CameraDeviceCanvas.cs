using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

public class CameraDeviceCanvas : MonoBehaviour
{
    [System.Serializable]
    public class CWebCamGroup
    {
        public string TargetWebCamName;
        public WebCamTexture TargetWebCamTexture;
        public bool CameraStatus;
    }

    [Header("Target Image")]
    public RawImage TargetImage;
    public RawImage loadingScreen;

    [Header("Main Settings")]
    public bool PlayCameraOnStart = true;
    public List<CWebCamGroup> WebCamGroup;

    [Header("Current Camera Device")]
    public int CurrentIndex = 0;
    public string CurrentCameraDevice;

    bool isInitialized = false;

    void WebInitialized()
    {
        if (!isInitialized)
        {
            WebCamDevice[] camDetected = WebCamTexture.devices;
            for (int i = 0; i < camDetected.Length; i++)
            {
                CWebCamGroup data = new CWebCamGroup();
                data.TargetWebCamName = camDetected[i].name;
                data.CameraStatus = false;
                WebCamGroup.Add(data);
            }
            isInitialized = true;
        }
    }

    private void Awake()
    {
        WebInitialized();
    }

    public void StopAllCameraDevice()
    {
        for (int i = 0; i < WebCamGroup.Count; i++)
        {
            if (WebCamGroup[i].TargetWebCamTexture != null && WebCamGroup[i].CameraStatus)
            {
                WebCamGroup[i].TargetWebCamTexture.Stop();
                WebCamGroup[i].CameraStatus = false;
            }
        }
    }

    public void NextCameraDevice()
    {
        if (CurrentIndex < WebCamGroup.Count - 1)
        {
            CurrentIndex++;
        }
        InvokeCameraDevicePlay(CurrentIndex);
    }

    public void PrevCameraDevice()
    {
        if (CurrentIndex > 1)
        {
            CurrentIndex--;
        }
        InvokeCameraDevicePlay(CurrentIndex);
    }

    public void InvokeCameraDevicePlay(int aIndex)
    {
        loadingScreen.gameObject.SetActive(false);
        StopAllCameraDevice();

        if (aIndex < WebCamGroup.Count)
        {
            if (WebCamGroup[aIndex].TargetWebCamTexture == null)
            {
                WebCamGroup[aIndex].TargetWebCamTexture = new WebCamTexture(WebCamGroup[aIndex].TargetWebCamName);
            }
            TargetImage.texture = WebCamGroup[aIndex].TargetWebCamTexture;
            TargetImage.material.mainTexture = WebCamGroup[aIndex].TargetWebCamTexture;
            WebCamGroup[aIndex].TargetWebCamTexture.Play();
            WebCamGroup[aIndex].CameraStatus = true;
            CurrentCameraDevice = WebCamGroup[aIndex].TargetWebCamName;
        }
    }

    public void InvokeCameraDeviceStop(int aIndex)
    {
        loadingScreen.gameObject.SetActive(true);
        if (aIndex < WebCamGroup.Count)
        {
            if (WebCamGroup[aIndex].TargetWebCamTexture != null)
            {
                WebCamGroup[aIndex].TargetWebCamTexture.Stop();
                WebCamGroup[aIndex].CameraStatus = false;
            }
        }
    }

    void PlayDefaultCamera()
    {
        InvokeCameraDevicePlay(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayCameraOnStart)
        {
            Invoke("PlayDefaultCamera", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
