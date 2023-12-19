using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tap : MonoBehaviour, IMixedRealityGestureHandler
{
    public GameObject Target;
    public GameObject MyGaze;

    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler>(this);
        MyGaze.SetActive(false);
    }

    private void OnDisable()
    {
        // Instruct Input System to disregard all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityGestureHandler>(this);
        MyGaze.SetActive(true);
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
        
        if (this.transform.childCount != 0)
        {
            WalkToTarget[] targets = GetComponentsInChildren<WalkToTarget>();
            foreach(WalkToTarget child in targets)
            {
                Destroy(child.gameObject);
            }
        }
        Debug.Log("Gesture completed: " + eventData.MixedRealityInputAction.Description);
        

        GameObject target = Instantiate(Target);
        target.transform.position = this.gameObject.GetComponent<UpdateTeleportCursor>().TeleportCursor.transform.position;
        target.transform.rotation = this.gameObject.GetComponent<UpdateTeleportCursor>().TeleportCursor.transform.rotation;

        target.transform.parent = this.transform;
    }

    public void OnGestureCanceled(InputEventData eventData)
    {
    }
}