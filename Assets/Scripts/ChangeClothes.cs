using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeClothes : MonoBehaviour
{
    public GameObject FittingModel = null;
    public int texNum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClickedChangeClothes()
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
        List<GameObject> modelClothess = FittingModel.GetComponent<PhotonFashionView>().fasions_object;
        FittingModel.GetComponent<PhotonFashionView>().tex_Num = this.texNum;
        Texture tex = this.gameObject.GetComponent<Renderer>().material.mainTexture;
        foreach (GameObject modelClothes in modelClothess)
        {
            if (modelClothes.transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
            {
                var clothesRenderer = modelClothes.GetComponent<Renderer>();
                clothesRenderer.material.mainTexture = tex;
            }
        }
    }
}
