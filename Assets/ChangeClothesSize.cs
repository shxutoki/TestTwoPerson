using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeClothesSize : MonoBehaviour
{
    public GameObject FittingModel = null;
    public int sizeNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClickedChangeSize()
    {
        PhotonFashionView[] photonFashionViews;
        photonFashionViews = GameObject.FindObjectsOfType<PhotonFashionView>();
        foreach (PhotonFashionView fashionview in photonFashionViews)
        {
            if (fashionview.gameObject.GetComponent<PhotonView>().IsMine)
            {
                FittingModel = fashionview.gameObject;
            }
        }
        for (int i = 0; i < FittingModel.GetComponent<PhotonFashionView>().fasions_vaild.Count; i++)
        {
            if (i == sizeNum)
            {
                FittingModel.GetComponent<PhotonFashionView>().fasions_vaild[i] = true;
                Debug.Log(i);
            }
            else
            {
                FittingModel.GetComponent<PhotonFashionView>().fasions_vaild[i] = false;
            }
        }
    }
}
