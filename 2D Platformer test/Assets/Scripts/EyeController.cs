using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: test leftOffset, make rightOffset and UpOffset

public class EyeController : MonoBehaviour
{

    public GameObject PlayerBody;

    public float PlayerPosX;
    public float PlayerPosY;
    //this is public to make sure that it's following correctly. make private if we have too many public variables later.
    public float EyePosX;
    public float EyePosY;
    //so that we can add the offset to the EyePos. They're seperate because they need to be floats so we can add to them.

    // Update is called once per frame
    void Update()
    {
        PlayerPosX = PlayerBody.transform.position.x;
        PlayerPosY = PlayerBody.transform.position.y;
        EyePosX = gameObject.transform.position.x;
        EyePosY = gameObject.transform.position.y;        

        Vector2 leftOffset = new(PlayerPosX + -2.0f, PlayerPosY);
        Vector2 rightOffset = new(PlayerPosX + 2.0f, PlayerPosY);
        Vector2 upOffset = new(PlayerPosX, PlayerPosY + 1.0f);
        //offset for the eyes.
        //NOTE: you can say "new Vector2()" instead of "new", and that's what intellicode will default to, but both work fine.

        gameObject.transform.position = PlayerBody.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position = leftOffset;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position = rightOffset;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {   
            gameObject.transform.position = upOffset;
        }
    }
}
