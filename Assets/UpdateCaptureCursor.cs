using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCaptureCursor : MonoBehaviour
{
    public GameObject captureCursor;
    // Start is called before the first frame update
    void Start()
    {
        captureCursor = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            captureCursor.transform.position = hitInfo.point;
        }
    }
}
