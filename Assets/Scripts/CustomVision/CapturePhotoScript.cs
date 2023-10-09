using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;
using System.IO;
using TMPro;

public class CapturePhotoScript : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    public GameObject textMesh;


    private string _filePath;

    private int captureCount = 0;

    // Use this for initialization
    public void OnCaptureButtonClicked()
    {
        CustomVisionAnalyser.Instance.PlaceAnalysisLabel();

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.9f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                string filename = string.Format(@"CapturedImage{0}.jpg", captureCount);
                _filePath = Path.Combine(Application.persistentDataPath, filename);
                captureCount++;
                // Take a picture
                photoCaptureObject.TakePhotoAsync(_filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
            });
        });
    }

    /// <summary>
    /// éBâeå„ÇÃï€ë∂èàóù
    /// </summary>
    /// <param name="result"></param>
    private void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            textMesh.GetComponent<TextMesh>().text = "success!";
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.LogError("Failed to save Photo to disk");
            return;
        }
    }

    /*void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into our target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a gameobject that we can apply our texture to
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));

        quad.transform.parent = this.transform;
        quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

        quadRenderer.material.SetTexture("_MainTex", targetTexture);

        // Deactivate our camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }*/

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        textMesh.GetComponent<TextMesh>().text += "Stopped photo capture.";

        // Shutdown our photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;

        StartCoroutine(CustomVisionAnalyser.Instance.AnalyseLastImageCaptured(_filePath));

    }

    public void SendImage()
    {
        string filePath = "D:/table.jpg";
        StartCoroutine(CustomVisionAnalyser.Instance.AnalyseLastImageCaptured(filePath));
    }
}