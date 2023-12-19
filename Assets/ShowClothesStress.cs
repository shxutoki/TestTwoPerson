using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowClothesStress : MonoBehaviour
{
    public bool show;
    public bool Init = false;
    // Start is called before the first frame update
    void Start()
    {
        show = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameObject.FindGameObjectsWithTag("Model").Length != 0) && (!Init))
        {
            GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
            foreach (GameObject model in models)
            {
                for (int i = 0; i < model.transform.childCount; i++)
                {
                    model.transform.GetChild(i).gameObject.GetComponent<OnFocusedShowIMG>().enabled = false;
                }

            }
            Init = true;
        }

    }

    public void OnClickShowClothesStress()
    {
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");
        foreach (GameObject model in models)
        {
            for (int i = 0; i < model.transform.childCount; i++)
            {
                model.transform.GetChild(i).gameObject.GetComponent<OnFocusedShowIMG>().enabled = show;
            }
            
        }
        show = !show;
    }
}
