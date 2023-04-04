using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorBlockCollision : MonoBehaviour
{

    public GameObject player;
    public colorChanger colorChanger;

    public SpriteRenderer spriteRenderer;
    public Vector4 red;
    public Vector4 blue;
    public Vector4 green;
    public Vector4 setColor;


    void Start()
    {
        setColor = spriteRenderer.color;
        red = colorChanger.colorList[1];
        green = colorChanger.colorList[2];
        blue = colorChanger.colorList[3];

    }

    // Update is called once per frame
    void Update()
    {
        if(colorChanger.colorNum == 1 && setColor == red || colorChanger.colorNum == 2 && setColor == green || colorChanger.colorNum == 3 && setColor == blue)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            spriteRenderer.color = setColor;
        }
    }
}
