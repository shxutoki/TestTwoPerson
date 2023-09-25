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
        Waving
    }

    Animator animator;

    AnimationType animationType;

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

    public void WalktoPosition(Vector3 newPosition)
    {
        IEnumerator coroutine = Walk(newPosition);
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
    public void InteractSit(GameObject obj)
    {
        //Sit(obj);
        IEnumerator coroutine = Sit(obj);
        StartCoroutine(coroutine);
    }
    private IEnumerator Sit(GameObject obj)
    //private void Sit(GameObject obj)
    {
        Vector3 newPosition = obj.transform.position;

        //if character is far from object, then go to the object.
        while (collision_range < Vector3.Distance(gameObject.transform.position, newPosition))
        {
            if (animationType != AnimationType.Walking)
            {
                animationType = AnimationType.Walking;
                SetAnimation(animationType);
            }
            gameObject.transform.LookAt(newPosition);
            gameObject.transform.position += gameObject.transform.forward * speed;
            yield return null;
        }
        animationType = AnimationType.Sitting;
        SetAnimation(animationType);
        gameObject.transform.LookAt(Camera.main.transform.position);
        gameObject.transform.position = obj.transform.position + gameObject.transform.forward * sit_to_cube;

    }


    public void SetAnimation(AnimationType animationType)
    {
        animator.SetInteger(animatorpara, (int)animationType);
    }
}