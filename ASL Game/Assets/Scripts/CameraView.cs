using UnityEngine;
using UnityEngine.UI;

public class CameraView : MonoBehaviour
{

    RawImage rawImage;
    AspectRatioFitter fitter;
    WebCamTexture webcamTexture;
    bool ratioSet;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        fitter = GetComponent<AspectRatioFitter>();
        InitWebCam();
    }

    void Update()
    {

        //if (webcamTexture.width > 100 && !ratioSet)
        //{
        //    ratioSet = true;
        //    SetAspectRatio();
        //}
    }

    void SetAspectRatio()
    {
        fitter.aspectRatio = (float)webcamTexture.width / (float)webcamTexture.height;
    }

    void InitWebCam()
    {
        string camName = WebCamTexture.devices[0].name;
        webcamTexture = new WebCamTexture(camName, Screen.width, Screen.height, 30);
        rawImage.texture = webcamTexture;
        webcamTexture.Play();
    }

    public WebCamTexture GetCamImage()
    {
        return webcamTexture;
    }
}
//using UnityEngine;
//using UnityEngine.UI;

//public class CameraView : MonoBehaviour
//{

//    RawImage rawImage;
//    AspectRatioFitter fitter;
//    WebCamTexture webcamTexture;
//    bool ratioSet;

//    void Start()
//    {
//        Debug.Log("Event Start");
//        Debug.Log("got raw image");
//        fitter = GetComponent<AspectRatioFitter>();
//        Debug.Log("got fitter");
//        InitWebCam();

//    }

//    void Update()
//    {

//        //if (webcamTexture.width > 100 && !ratioSet)
//        //{
//        //    ratioSet = true;
//        //    SetAspectRatio();
//        //}
//    }

//    void SetAspectRatio()
//    {
//        fitter.aspectRatio = (float)webcamTexture.width / (float)webcamTexture.height;
//    }

//    void InitWebCam()
//    {
//        string camName = WebCamTexture.devices[0].name;
//        Debug.Log("got cam");
//        Debug.Log(camName);
//        WebCamTexture webcamTexture = new WebCamTexture(camName);
//        GetComponent<RawImage>().texture = webcamTexture;
//        Debug.Log("assigned texture");
//        webcamTexture.Play();
//    }

//    public WebCamTexture GetCamImage()
//    {
//        return webcamTexture;
//    }
//}



////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class CameraView : MonoBehaviour
////{
////    // Start is called before the first frame update
////    void Start()
////    {

////    }

////    // Update is called once per frame
////    void Update()
////    {

////    }
////}
