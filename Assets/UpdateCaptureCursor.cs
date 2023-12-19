using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCaptureCursor : MonoBehaviour
{
    GameObject captureCursor;
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
            if (hitInfo.collider.gameObject.layer == 31)
            {
                // If the raycast hit a hologram, use that as the focused object.
                captureCursor.transform.position = hitInfo.point;
                Vector3 lookat = headPosition;
                captureCursor.transform.LookAt(lookat);
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                captureCursor.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
                Vector3 lookat = headPosition;
                captureCursor.transform.LookAt(lookat);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            captureCursor.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
            Vector3 lookat = headPosition;
            captureCursor.transform.LookAt(lookat);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
