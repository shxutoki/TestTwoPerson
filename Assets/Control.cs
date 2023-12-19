using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject model in models)
        {
            if (model.GetComponent<MotionManager>().isActiveAndEnabled)
            {
                model.GetComponent<MotionManager>().enabled = false;
            }
            if (model.GetComponentInChildren<OnFocusedShowIMG>().isActiveAndEnabled)
            {
                model.GetComponentInChildren<OnFocusedShowIMG>().enabled = false;
            }
        }
    }
}
