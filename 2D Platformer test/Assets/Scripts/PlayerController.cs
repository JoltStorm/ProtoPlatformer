using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public float PlayerMoveSpeed;
    private bool RightKeyDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightKeyDown = true;

            while (RightKeyDown)
            {
                player.transform.Translate(PlayerMoveSpeed, 0, 0);
            }
        } 
        else
        {
            RightKeyDown = false;
        }
    }

}
