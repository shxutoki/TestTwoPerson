using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMultiCharacterAnimation : MonoBehaviour
{
    public List<GameObject> objs;
    int num = 0;
    // Start is called before the first frame update

    public void ChangeAnimation()
    {
        num++;
        if (num == 4)
        {
            num = 0;
        }
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<Animator>().SetInteger("motion", num);
        }
    }

    public void EnableDisable()
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(!obj.active);
            obj.GetComponent<Animator>().SetInteger("motion", num);
        }
    }
}
