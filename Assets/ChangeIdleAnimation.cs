using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIdleAnimation : MonoBehaviour
{

    GameObject MyAvatar;
    int animationNum = 11;
    int idleNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeIdleAnimation()
    {
        GameObject[] avatars = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject avatar in avatars)
        {
            if (avatar.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                MyAvatar = avatar;
                MyAvatar.GetComponent<MotionManager>().SetAnimationint(animationNum);
                MyAvatar.GetComponent<Photon.Pun.PhotonFashionViewNew>().motion = animationNum;
                animationNum++;
                if (animationNum > 23)
                {
                    animationNum = 11;
                }
            }
        }
    }

    public void OnChangeIdle()
    {
        GameObject[] avatars = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject avatar in avatars)
        {
            if (avatar.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                MyAvatar = avatar;
                MyAvatar.GetComponent<MotionManager>().SetAnimationint(idleNum);
                MyAvatar.GetComponent<Photon.Pun.PhotonFashionViewNew>().motion = idleNum;
            }
        }
    }
}
