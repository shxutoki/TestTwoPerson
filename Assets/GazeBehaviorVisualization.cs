using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EyeData
{
    public Vector3 HeadPos;
    public Vector3 GazePos;

    public EyeData(Vector3 newHeadPos, Vector3 newGazePos)
    {
        HeadPos = newHeadPos;
        GazePos = newGazePos;
    }
}

public class Heatmap
{
    public Vector3 heatPoint;
    public float heatIntensity;

    public Heatmap(Vector3 newHeatPoint, float newHeatIntensity)
    {
        heatPoint = newHeatPoint;
        heatIntensity = newHeatIntensity;
    }
}


public class GazeBehaviorVisualization : MonoBehaviour
{
    public List<EyeData> eyedata = new List<EyeData>();
    public List<Heatmap> heatmap = new List<Heatmap>();

    public GameObject HeadVis;
    public GameObject GazepointVis;
    GameObject sphereHeatPoint;

    private Vector3 prevGaze;
    private int intensity = 0;

    public GameObject SphereHeatPrefab;

    private Stack<GameObject> SphereCollection = new Stack<GameObject>();
    private Queue<GameObject> SphereQueue = new Queue<GameObject>();

    private MeshRenderer HololensRenderer;

    public Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;

    private int frameCount;
    private const float neighborThreshold = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        gradient = new Gradient();

        // set from what key it changes
        colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.green;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.red;
        colorKey[2].time = 1.0f;

        // always opaque
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set prevGaze, if prevGaze is null, set current GazePoint as prevGaze
        if (prevGaze == null)
        {
            prevGaze = GazepointVis.transform.position;
        }

        //dist between current and previous gaze
        float dist = Vector3.Distance(prevGaze, GazepointVis.transform.position);

        
        //if dist > neighborThreshold, add a new indicator; else add the intensity of current indicator
        if (dist >= neighborThreshold)
        {
            
            intensity = 0;
            heatmap.Add(new Heatmap(prevGaze, intensity));
            sphereHeatPoint = Instantiate(SphereHeatPrefab, GazepointVis.transform.position, Quaternion.identity);
            SphereQueue.Enqueue(sphereHeatPoint);
            /*if (SphereCollection.Count == 0)
            {
                SphereCollection.Push(sphereHeatPoint);
            }
            else
            {
                GameObject prevSphereHeatPoint = SphereCollection.Pop();
                if (Vector3.Distance(sphereHeatPoint.transform.position, prevSphereHeatPoint.transform.position) < 0.1f)
                {
                    Destroy(sphereHeatPoint);
                }
                else
                {
                    SphereCollection.Push(prevSphereHeatPoint);
                    SphereCollection.Push(sphereHeatPoint);
                }
            }*/
        }
        else
        {
            if (intensity < 50)
            {
                intensity++;
            }
            else
            {
                intensity = 50;
            }
        }
        Color color = gradient.Evaluate(intensity / 50f);
        if (sphereHeatPoint != null)
        {
            sphereHeatPoint.GetComponentInChildren<Renderer>().material.color = color;
        }
        prevGaze = GazepointVis.transform.position;
    }

    public void OnReviewGaze()
    {
        foreach (GameObject gaze in SphereQueue)
        {
            gaze.GetComponent<TransparentChange>().ChangeToOpaque();
        }
    }
}
