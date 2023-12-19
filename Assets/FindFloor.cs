using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFloor : MonoBehaviour
{
    GameObject floorPlane;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public bool setFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        floorPlane = GameObject.Find("JetHolder").transform.Find("Plane").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((floorPlane.activeSelf) && (!setFloor))
        {
            this.gameObject.transform.SetParent(floorPlane.transform);
            this.gameObject.transform.localPosition = localPosition;
            this.gameObject.transform.localRotation = localRotation;
            setFloor = true;
        }
    }
}
