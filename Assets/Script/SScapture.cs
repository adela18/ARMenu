using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SScapture : MonoBehaviour
{
    public void TombolScreenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/MyScreenShot.png");
    }
}
