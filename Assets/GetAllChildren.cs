using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllChildren : MonoBehaviour
{
    public RuntimeAnimatorController animation;
    // Start is called before the first frame update
    void Start()
    {
        GetAllChildrenAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllChildrenAnimator()
    {
        int ChildNum = this.transform.childCount;
        for (int i = 0; i < ChildNum; i++)
        {
            GameObject childobj = this.transform.GetChild(i).gameObject;
            childobj.GetComponent<Animator>().runtimeAnimatorController = animation;
        }
    }
}
