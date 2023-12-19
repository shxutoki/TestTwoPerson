using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class UpdateTeleportCursor : MonoBehaviour
{
    public GameObject TeleportCursor;
    public float maxDistance = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;
        if (eyeGazeProvider != null)
        {
            //HeadVis.transform.position = Camera.main.transform.position;//eyeGazeProvider.GazeOrigin;
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.transform.position/*eyeGazeProvider.GazeOrigin*/, eyeGazeProvider.GazeDirection, out hitInfo);
            //Debug.Log(hitInfo.point);
            if (hitInfo.point != Vector3.zero)
            {
                /*if (hitInfo.collider.gameObject.name == "Plane")
                {
                    TeleportCursor.transform.position = hitInfo.point;

                }*/
                if ((hitInfo.collider.gameObject.layer == 31) || (hitInfo.collider.gameObject.name == "Plane"))
                {
                    TeleportCursor.transform.position = hitInfo.point;

                }
            }
            else
            {
                Debug.Log("No cast.");
            }
        }
    }
}
