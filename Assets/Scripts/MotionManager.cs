using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    public string animatorpara;
    //Animation Types
    public enum AnimationType
    {
        Idle = 0,
        Sitting,
        Walking,
        Waving,
    }

    Animator animator;

    public AnimationType animationType;

    public Vector3 targetPosition;
    public float collision_range = 1.0f;
    public float speed = 0.01f;
    public float sit_to_cube = 0.2f;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WalktoPosition(IEnumerator coroutine, Vector3 newPosition)
    {
        coroutine = Walk(newPosition);
        StartCoroutine(coroutine);
    }

    private IEnumerator Walk(Vector3 newPosition)
    {

        //float y = GameObject.Find("coordinate").GetComponent<Transform>().position.y;
        float ori_local_y = gameObject.transform.localPosition.y;

        //if the character is far from new position, walk to new position.
        while (collision_range < Vector3.Distance(gameObject.transform.position, new Vector3(newPosition.x, gameObject.transform.position.y, newPosition.z)))
        {
            if (animationType != AnimationType.Walking)
            {
                animationType = AnimationType.Walking;
                SetAnimation(animationType);
                gameObject.transform.LookAt(new Vector3(newPosition.x, gameObject.transform.position.y, newPosition.z));
            }
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ori_local_y, gameObject.transform.localPosition.z);
            yield return null;

        }
        //if the character is near new position, stop and idle.
        {
            animationType = AnimationType.Idle;
            SetAnimation(animationType);
        }
    }
    public void InteractSit(IEnumerator coroutine, Vector3 newPosition)
    {
        coroutine = Sit(newPosition);
        StartCoroutine(coroutine);
    }
    private IEnumerator Sit(Vector3 newPosition)
    //private void Sit(GameObject obj)
    {
        //float y = GameObject.Find("coordinate").GetComponent<Transform>().position.y;
        float ori_local_y = gameObject.transform.localPosition.y;

        //if the character is far from new position, walk to new position.
        while (collision_range < Vector3.Distance(gameObject.transform.position, new Vector3(newPosition.x, gameObject.transform.position.y, newPosition.z)))
        {
            if (animationType != AnimationType.Walking)
            {
                animationType = AnimationType.Walking;
                SetAnimation(animationType);
                gameObject.transform.LookAt(new Vector3(newPosition.x, gameObject.transform.position.y, newPosition.z));
            }
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ori_local_y, gameObject.transform.localPosition.z);
            yield return null;

        }
        //if the character is near new position, stop and sit.
        {
            animationType = AnimationType.Sitting;
            SetAnimation(animationType);
            Vector3 lookat = new Vector3(Camera.main.transform.position.x, this.gameObject.transform.position.y, Camera.main.transform.position.z);
            gameObject.transform.LookAt(lookat);
            gameObject.transform.position = new Vector3(newPosition.x, this.gameObject.transform.position.y, newPosition.z) + gameObject.transform.forward * 0.4f;
        }
    }
    


    public void SetAnimation(AnimationType animationType)
    {
        animator.SetInteger(animatorpara, (int)animationType);
        Debug.Log("set animation: " + animationType.ToString());
    }
}