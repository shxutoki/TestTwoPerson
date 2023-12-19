using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DrawCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCube()
    {
        //Draw a cube acting like a visible boundingBox for the recognized object
        GameObject objBoundingBox = DrawInSpace.Instance.DrawCube(1, 1, 1);
        objBoundingBox.transform.parent = transform;
        
        CustomVisionAnalyser.Instance.MakeBoundingBoxInteractable(objBoundingBox);
        //objBoundingBox.layer = 2;

        objBoundingBox.transform.SetParent(transform);
    }
}
