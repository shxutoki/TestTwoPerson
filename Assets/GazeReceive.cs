using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

public class GazeReceive : MonoBehaviour
{
    public GameObject GazePointVis;
    public GameObject HeadVis;

    public void OnDataReceived(Message message)
    {
        HeadVis.transform.localPosition = new Vector3((float)(message.values[0]), (float)message.values[1], (float)message.values[2]);
        GazePointVis.transform.localPosition = new Vector3((float)(message.values[3]), (float)message.values[4], (float)message.values[5]);
        // address
        var msg = message.address + ": ";

        // timestamp
        msg += "(" + message.timestamp.ToLocalTime() + ") ";

        // values
        foreach (var value in message.values)
        {
            msg += value.GetString() + " ";
        }



        Debug.Log(msg);
    }

    public void OnServerStarted(int port)
    {
        Debug.Log($"<color=blue>Start Server (port: {port})</color>");
    }

    public void OnServerStopped(int port)
    {
        Debug.Log($"<color=blue>Stop Server (port: {port})</color>");
    }
}
