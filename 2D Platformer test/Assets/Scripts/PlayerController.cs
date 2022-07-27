using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public float PlayerMoveSpeed;
    public float PlayerJumpHeight;
    private bool IsPlayerTouchingFloor = true;

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsPlayerTouchingFloor == true)
            {
                player.transform.Translate(new Vector2(0, PlayerJumpHeight));
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.Translate(new Vector2(0, -PlayerJumpHeight));
        }

    }
}
