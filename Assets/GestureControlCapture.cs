using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureControlCapture : MonoBehaviour, IMixedRealityGestureHandler
{
    public GameObject photoCapture;

    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler>(this);
    }

    private void OnDisable()
    {
        // Instruct Input System to disregard all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityGestureHandler>(this);
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        Debug.Log("Gesture started: " + eventData.MixedRealityInputAction.Description);
    }

    public void OnGestureUpdated(InputEventData eventData)
    {

    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        photoCapture.GetComponent<CapturePhotoScript>().OnCaptureButtonClicked();
    }

    public void OnGestureCanceled(InputEventData eventData)
    {
    }
}