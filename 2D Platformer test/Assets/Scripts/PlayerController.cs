using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public float PlayerMoveSpeed;
//    private bool RightKeyDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.Translate(new Vector2(PlayerMoveSpeed, 0));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            player.transform.Translate(new Vector2(-PlayerMoveSpeed, 0));
        }

    }

}
