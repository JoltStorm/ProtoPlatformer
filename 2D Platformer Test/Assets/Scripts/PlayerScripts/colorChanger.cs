using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour
{
    [Header("color changing")]
    public Vector4[] colorList;
    public SpriteRenderer spriteRenderer;

    [Header("collision vars")]
    public float colorNum = 0;

    void Start()
    {
        spriteRenderer.color = colorList[0];
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            spriteRenderer.color = colorList[0];
            colorNum = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            spriteRenderer.color = colorList[1];
            colorNum = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            spriteRenderer.color = colorList[2];
            colorNum = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            spriteRenderer.color = colorList[3];
            colorNum = 3;
        }
    }
}
