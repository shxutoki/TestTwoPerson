using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using uOSC;

public class GazeSend : MonoBehaviour
{
    public GameObject GazePointVis;
    public GameObject HeadVis;

    public float defaultDistanceInMeters = 2;
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
            HeadVis.transform.position = Camera.main.transform.position;//eyeGazeProvider.GazeOrigin;
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.transform.position/*eyeGazeProvider.GazeOrigin*/, eyeGazeProvider.GazeDirection, out hitInfo);
            if (hitInfo.point != Vector3.zero)
            {
                GazePointVis.transform.position = hitInfo.point;
            }
            else
            {
                GazePointVis.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection.normalized * defaultDistanceInMeters;
            }
            
            var client = GetComponent<uOscClient>();
            client.Send("eye_data", HeadVis.transform.localPosition.x, HeadVis.transform.localPosition.y, HeadVis.transform.localPosition.z, GazePointVis.transform.localPosition.x, GazePointVis.transform.localPosition.y, GazePointVis.transform.localPosition.z);
        }
    }

    public void OnClientStarted(string address, int port)
    {
        Debug.Log($"<color=red>Start Client (address: {address}, port: {port})</color>");
    }

    public void OnClientStopped(string address, int port)
    {
        Debug.Log($"<color=red>Stop Client (address: {address}, port: {port})</color>");
    }

    public void SetPartnerIp(string ip)
    {
        GetComponent<uOscClient>().address = ip;
    }
}
