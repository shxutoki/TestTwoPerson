using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class Tap : MonoBehaviour, IMixedRealityGestureHandler
{
    public GameObject myGaze;
    public GameObject Target;

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
        Debug.Log("Gesture completed: " + eventData.MixedRealityInputAction.Description);
        Instantiate(Target, myGaze.transform.position, myGaze.transform.rotation);
    }

    public void OnGestureCanceled(InputEventData eventData)
    {
    }
}