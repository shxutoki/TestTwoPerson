using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInfrontUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var headPosition = Camera.main.transform.position;


        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
        Vector3 lookat = headPosition;
        transform.LookAt(lookat);
        transform.Rotate(0, 180, 0);

    }

}
