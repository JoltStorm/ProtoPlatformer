using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: test leftOffset, make rightOffset and UpOffset

public class EyeController : MonoBehaviour
{

    public GameObject PlayerBody;
    public GameObject PlayerEyes;

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

        Vector3 idleOffset = new(PlayerPosX, PlayerPosY, -0.2f);
        Vector3 leftOffset = new(PlayerPosX + -2.0f, PlayerPosY, -0.2f);
        Vector3 rightOffset = new(PlayerPosX + 2.0f, PlayerPosY, -0.2f);
        Vector3 upOffset = new(PlayerPosX, PlayerPosY + 1.0f, -0.2f);
        Vector3 downOffset = new(PlayerPosX, PlayerPosY - 2.0f, -0.2f);

        //offset for the eyes.
        //NOTE: you can say "new Vector2()" instead of "new", and that's what intellicode will default to, but both work fine.
        //NOTE 2: the "-0.2f" is there so the eyes appear in front of the body.

        gameObject.transform.position = idleOffset;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = leftOffset;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = rightOffset;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {   
            gameObject.transform.position = upOffset;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position = downOffset;
        }
    }
}
