using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTshirtCollection : MonoBehaviour
{
    public GameObject Tshirt;
    public List<Texture2D> TshirtIMGs;
    string folderPath = "IMGs";
    public int StartNUM;
    // Start is called before the first frame update
    void Start()
    {
        var textures = Resources.LoadAll(folderPath, typeof(Texture2D));
        Debug.Log(textures.Length);
        foreach (var texture in textures)
        {
            TshirtIMGs.Add((Texture2D)texture);
        }
        GenerateTshirts();
    }

    // Update is called once per frame
    void Update()
    {
            

    }

    public void GenerateTshirts()
    {
        int x = -2;
        int y = 1;
        int z = -1;
        for (int i = StartNUM; i < 20 + StartNUM; i++)
        {
            GameObject InstantiatedTshirt = Instantiate(Tshirt, this.transform);
            InstantiatedTshirt.GetComponent<Renderer>().material.mainTexture = TshirtIMGs[i];
            InstantiatedTshirt.GetComponent<ChangeClothes>().texNum = i;
            InstantiatedTshirt.transform.localPosition = new Vector3(x * 4, y * 4, z * 10);
            if (z > 0)
            {
                InstantiatedTshirt.transform.Rotate(new Vector3(0, 180, 0));
            }
            x = x + 1;
            if (x > 2)
            {
                x = -2;
                y = y - 1;
            }
            if (y < 0)
            {
                z = 1;
                y = 1;
            }
        }
    }
}
