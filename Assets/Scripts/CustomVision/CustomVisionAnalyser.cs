using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;

public class CustomVisionAnalyser : MonoBehaviour
{
    /// <summary>
    /// Unique instance of this class
    /// </summary>
    public static CustomVisionAnalyser Instance;

    public float probabilityThreshold = 0.7f;


    public GameObject CaptureCursor;
    public GameObject WaitForResult;

    public GameObject Appbar;

    /// <summary>
    /// Insert your Prediction Key here
    /// </summary>
    private string predictionKey = "7b2cf5288ca1429ca09d986d88f94970";

    /// <summary>
    /// Insert your prediction endpoint here
    /// </summary>
    private string predictionEndpoint;

    /// <summary>
    /// Byte array of the image to submit for analysis
    /// </summary>
    [HideInInspector] public byte[] imageBytes;



    /// <summary>
    /// The quad object hosting the imposed image captured
    /// </summary>
    GameObject quad;

    /// <summary>
    /// Renderer of the quad object
    /// </summary>
    internal Renderer quadRenderer;

    private BoundingBox bbox;


    /// <summary>
    /// Initialises this class
    /// </summary>
    private void Awake()
    {
        Instance = this;
        predictionEndpoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/c4ddd194-5352-44cb-8c50-55804f716121/detect/iterations/Iteration9/image";
    }

    /// <summary>
    /// Call the Computer Vision Service to submit the image.
    /// </summary>
    public IEnumerator AnalyseLastImageCaptured(string imagePath)
    {

        //Makes call to the API to analyse the picture and find a tag
        //When it is done, SceneOrganiser.Instance.FinaliseLabel is called
        WWWForm webForm = new WWWForm();
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(predictionEndpoint, webForm))
        {
            // Gets a byte array out of the saved image
            imageBytes = GetImageAsByteArray(imagePath);

            unityWebRequest.SetRequestHeader("Content-Type", "application/octet-stream");
            unityWebRequest.SetRequestHeader("Prediction-Key", predictionKey);

            // The upload handler will help uploading the byte array with the request
            unityWebRequest.uploadHandler = new UploadHandlerRaw(imageBytes);
            unityWebRequest.uploadHandler.contentType = "application/octet-stream";

            // The download handler will help receiving the analysis from Azure
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.error != "")
                Debug.Log(unityWebRequest.error);

            string jsonResponse = unityWebRequest.downloadHandler.text;

            Debug.Log("response: " + jsonResponse);

            //textMesh.GetComponent<TextMesh>().text = jsonResponse;

            //create a texture
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(imageBytes);
            quadRenderer.material.SetTexture("_MainTex", tex);

            // The response will be in JSON format, therefore it needs to be deserialized  
            
            AnalysisObject analysisObject = JsonConvert.DeserializeObject<AnalysisObject>(jsonResponse);

            FinaliseLabel(analysisObject);

            CaptureCursor.SetActive(true);
            WaitForResult.SetActive(false);

        }
    }

    /// <summary>
    /// Returns the contents of the specified image file as a byte array.
    /// </summary>
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

        BinaryReader binaryReader = new BinaryReader(fileStream);

        return binaryReader.ReadBytes((int)fileStream.Length);
    }

    /// <summary>
    /// Set the Tags as Text of the last label created. 
    /// </summary>
    public void FinaliseLabel(AnalysisObject analysisObject)
    {
        if (analysisObject.Predictions != null)
        {
            // Sort the Predictions to locate the highest one
            List<Prediction> sortedPredictions = new List<Prediction>();
            sortedPredictions = analysisObject.Predictions.OrderBy(p => p.Probability).ToList();
            Prediction bestPrediction = new Prediction();
            bestPrediction = sortedPredictions[sortedPredictions.Count - 1];

            Debug.Log(bestPrediction.Probability);
            Debug.Log(bestPrediction.TagName);


            if (bestPrediction.Probability > probabilityThreshold)
            {
                // The prediction is considered good enough
                quadRenderer = quad.GetComponent<Renderer>() as Renderer;
                Bounds quadBounds = quadRenderer.bounds;

                //Draw a cube acting like a visible boundingBox for the recognized object
                GameObject objBoundingBox = DrawInSpace.Instance.DrawCube((float)bestPrediction.BoundingBox.Width, (float)bestPrediction.BoundingBox.Height);
                objBoundingBox.transform.parent = quad.transform;
                objBoundingBox.transform.localPosition = CalculateBoundingBoxPosition(quadBounds, bestPrediction.BoundingBox);
                objBoundingBox.layer = 2;

                objBoundingBox.transform.SetParent(transform);
                MakeBoundingBoxInteractable(objBoundingBox);

                if (bestPrediction.TagName == "Chair")
                {
                    GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
                    foreach (GameObject model in models)
                    {
                        if (model.gameObject.GetPhotonView().IsMine)
                        {
                            IEnumerator coroutine = null;
                            model.GetComponent<MotionManager>().InteractSit(coroutine, objBoundingBox.transform.position);
                        }
                    }
                }
            }
        }


    }

    /// <summary>
    /// Instantiate a label in the appropriate location relative to the Main Camera.
    /// </summary>
    public void PlaceAnalysisLabel()
    {
        if (GameObject.Find("Label"))
        {
            Destroy(GameObject.Find("Label"));
        }
        GameObject panel = new GameObject("Label");
        TextMeshPro label = panel.AddComponent<TextMeshPro>();
        label.fontSizeMin = 1;
        label.fontSizeMax = 70;
        label.enableAutoSizing = true;
        label.rectTransform.sizeDelta = new Vector2(2, 1);
        label.transform.localScale = new Vector3(0.1f, 0.1f, 0.01f);
        label.alignment = TextAlignmentOptions.Midline;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            label.transform.position = hitInfo.point;
            label.transform.Translate(Vector3.back * 0.1f, Space.World);
            label.transform.Translate(Vector3.up * 0.1f, Space.World);
            label.transform.rotation = Quaternion.LookRotation(gazeDirection);

        }
        if (GameObject.Find("IMGQuad"))
        {
            Destroy(GameObject.Find("IMGQuad"));
        }
        // Create a GameObject to which the texture can be applied
        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = "IMGQuad";

        quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        Material m = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
        quadRenderer.material = m;

        // Here you can set the transparency of the quad. Useful for debugging
        // Allows you to see the picture taken in the real world
        float transparency = 0.0f;
        quadRenderer.material.color = new Color(1, 1, 1, transparency);

        //Set the position and scale of the quad depending on user position
        quad.transform.SetParent(transform);

        //Set the quad (screen for picture) at the cursor position and make it face the user
        quad.transform.position = GetCursorPositionOnMesh();
        quad.transform.rotation = Quaternion.LookRotation(gazeDirection);

        // The quad scale has been set with the following value following experimentation,  
        // to allow the image on the quad to be as precisely imposed to the real world as possible
        quad.transform.localScale = new Vector3(hitInfo.distance, 1.65f / 3f * hitInfo.distance, 1f);

        quad.transform.parent = null;
    }

    public void SetLastLabel(string text)
    {
        if (text != "Unknown")
        {

        }
    }

    /// <summary>
    /// This method hosts a series of calculations to determine the position 
    /// of the Bounding Box on the quad created in the real world
    /// by using the Bounding Box received back alongside the Best Prediction
    /// </summary>
    public Vector3 CalculateBoundingBoxPosition(Bounds b, BoundingBox2D boundingBox)
    {
        Debug.Log($"BB: left {boundingBox.Left}, top {boundingBox.Top}, width {boundingBox.Width}, height {boundingBox.Height}");

        double centerFromLeft = boundingBox.Left + (boundingBox.Width / 2);
        double centerFromTop = boundingBox.Top + (boundingBox.Height / 2);
        Debug.Log($"BB CenterFromLeft {centerFromLeft}, CenterFromTop {centerFromTop}");

        double quadWidth = b.size.normalized.x;
        double quadHeight = b.size.normalized.y;
        Debug.Log($"Quad Width {b.size.normalized.x}, Quad Height {b.size.normalized.y}");

        double normalisedPos_X = (quadWidth * centerFromLeft) - (quadWidth / 2);
        double normalisedPos_Y = (quadHeight * centerFromTop) - (quadHeight / 2);

        return new Vector3((float)normalisedPos_X, (float)normalisedPos_Y, 0);
    }

    public Vector3 GetCursorPositionOnMesh()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            return hitInfo.point;
        }
        else
        {
            Debug.Log("No mesh hit\n");

            return new Vector3(0, 0, 0);
        }
    }

    public void MakeBoundingBoxInteractable(GameObject obj)
    {
        Rigidbody body = obj.AddComponent<Rigidbody>();
        body.mass = 100;
        body.useGravity = false;
        body.isKinematic = true;
        BoundingBox bb = obj.AddComponent<BoundingBox>();
        bb.Target = obj;
        bb.BoundsOverride = obj.GetComponent<BoxCollider>();
        bb.BoxMaterial = obj.GetComponent<Renderer>().material;
        bb.BoxGrabbedMaterial = obj.GetComponent<Renderer>().material;
        GameObject appbar = Instantiate(Appbar);
        appbar.transform.localScale = new Vector3(2, 2, 2);
        appbar.layer = 8;
        appbar.GetComponent<AppBar>().Target = obj.GetComponent<BoundingBox>();
    }
}