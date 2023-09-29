using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStatusController : MonoBehaviour
{
    public GameObject InteractionStatus;
    public GameObject TshirtCollection;
    // Start is called before the first frame update
    void Start()
    {
        InteractionStatus.SetActive(false);
        TshirtCollection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractionStatus()
    {
        InteractionStatus.SetActive(true);
        TshirtCollection.SetActive(false);
    }

    public void SetTshirtCollection()
    {
        InteractionStatus.SetActive(false);
        TshirtCollection.SetActive(true);
    }
}
