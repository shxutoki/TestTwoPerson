using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public float speed = 1.0f; //定义速度参数

    public bool shouldMove = false;

    public Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMove)
        {
            transform.LookAt(newPosition);
            // 移动物体到目标位置
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);


            // 检查物体是否到达目标位置
            if (Vector3.Distance(transform.position, newPosition) < 0.001f)
            {
                shouldMove = false;
            }
        }
    }

    public void MoveToPoint(Vector3 targetPosition)
    {
        newPosition = targetPosition;
        shouldMove = true;
    }
}
