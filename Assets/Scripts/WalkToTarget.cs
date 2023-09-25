using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WalkToTarget : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Walk();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Walk()
    {
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject model in models)
        {
            if (model.gameObject.GetPhotonView().IsMine)
            {
                model.GetComponent<MotionManager>().WalktoPosition(this.transform.position);
            }
        }
    }
}