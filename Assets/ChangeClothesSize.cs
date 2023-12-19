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
        PhotonFashionViewNew[] photonFashionViewNews;
        photonFashionViewNews = GameObject.FindObjectsOfType<PhotonFashionViewNew>();
        foreach (PhotonFashionViewNew fashionviewnew in photonFashionViewNews)
        {
            if (fashionviewnew.gameObject.GetComponent<PhotonView>().IsMine)
            {
                FittingModel = fashionviewnew.gameObject;
            }
        }
        for (int i = 0; i < FittingModel.GetComponent<PhotonFashionViewNew>().fasions_vaild.Count; i++)
        {
            if (i == sizeNum)
            {
                FittingModel.GetComponent<PhotonFashionViewNew>().fasions_vaild[i] = true;
                Debug.Log(i);
            }
            else
            {
                FittingModel.GetComponent<PhotonFashionViewNew>().fasions_vaild[i] = false;
            }
        }
    }
}
