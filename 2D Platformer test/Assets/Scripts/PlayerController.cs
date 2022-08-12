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
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * PlayerMoveSpeed);
        }
    }
}
