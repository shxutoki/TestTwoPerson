using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAvatarFaceToMe : MonoBehaviour
{
    GameObject MyAvatar;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnMakeAvatarFaceToMe()
    {
        GameObject[] avatars = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject avatar in avatars)
        {
            if (avatar.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                MyAvatar = avatar;
            }
        }
        Vector3 lookat = new Vector3(Camera.main.transform.position.x, MyAvatar.transform.position.y, Camera.main.transform.position.z);
        MyAvatar.transform.LookAt(lookat);
    }
}
