using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentChange : MonoBehaviour
{
    private float transparency = 1;
    private int timeCount = 0;
    Renderer objectRenderer;

    public bool isReviewing = false;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();

        // 确保材质支持透明度
        objectRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objectRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objectRenderer.material.SetInt("_ZWrite", 0);
        objectRenderer.material.DisableKeyword("_ALPHATEST_ON");
        objectRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        objectRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objectRenderer.material.renderQueue = 3000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isReviewing)
        {
            float transparencyRatio = timeCount / 100f;
            Color color = objectRenderer.material.color;
            color.a = transparency - transparencyRatio;

            objectRenderer.material.color = color;

            timeCount++;
            if (timeCount >= 100)
            {
                timeCount = 100;
            }
        }
    }

    public void ChangeToOpaque()
    {
        isReviewing = true;
        Color color = objectRenderer.material.color;
        color.a = 1;

        objectRenderer.material.color = color;
    }

    public void ReviewOver()
    {
        isReviewing = false;
    }
}
