using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class OnFocusedShowIMG : MonoBehaviour, IMixedRealityFocusHandler
{
    public GameObject FittedIMG;
    

    public void OnFocusEnter(FocusEventData eventData)
    {
        FittedIMG.SetActive(true);
        Vector3 lookat = new Vector3(Camera.main.transform.position.x, FittedIMG.transform.position.y, Camera.main.transform.position.z);
        FittedIMG.transform.LookAt(lookat);
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        
    }


    void Start()
    {
        FittedIMG.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        FittedIMG.transform.localPosition = new Vector3(0.8f, 0.8f, 0);
    }

}
