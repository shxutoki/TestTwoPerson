using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeRayVisualize : MonoBehaviour
{
    public GameObject GazePointVis;
    public GameObject HeadVis;
    public Material lineMaterial;

    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = 0.001f;
        lr.endWidth = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (HeadVis == null || GazePointVis == null)
        {
            return;
        }
        //Debug.Log(transform.parent.gameObject.name + HeadVis.transform.position);
        lr.SetPosition(0, HeadVis.transform.position);
        lr.SetPosition(1, GazePointVis.transform.position);
    }
}
