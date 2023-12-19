using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class OnFocusAway : MonoBehaviour, IMixedRealityFocusHandler
{
    public void OnFocusEnter(FocusEventData eventData)
    {
        Debug.Log("Focusing IMG");
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        Debug.Log("Focus away");
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

    }
}
