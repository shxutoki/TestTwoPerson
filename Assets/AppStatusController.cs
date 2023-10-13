using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStatusController : MonoBehaviour
{
    public GameObject InteractionStatus;
    public GameObject TshirtCollection;
    public GameObject CaptureHandler;
    public GameObject captureCursor;
    // Start is called before the first frame update
    void Start()
    {
        InteractionStatus.SetActive(false);
        TshirtCollection.SetActive(false);
        CaptureHandler.SetActive(false);
        captureCursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractionStatus()
    {
        InteractionStatus.SetActive(true);
        TshirtCollection.SetActive(false);
        CaptureHandler.SetActive(false);
        captureCursor.SetActive(false);
    }

    public void SetTshirtCollection()
    {
        InteractionStatus.SetActive(false);
        TshirtCollection.SetActive(true);
        CaptureHandler.SetActive(false);
        captureCursor.SetActive(false);
    }

    public void SetCapturePhoto()
    {
        CaptureHandler.SetActive(true);
        InteractionStatus.SetActive(false);
        TshirtCollection.SetActive(false);
        captureCursor.SetActive(true);
    }
}
