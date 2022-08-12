using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel.
    public Vector2 PlayerMoveSpeed;
    public Vector2 PlayerJumpHeight;
    
    private bool IsGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsGrounded = Physics.Raycast(transform.position, Vector2.down, 10f);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-PlayerMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(PlayerMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded == true)
        {
            rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
            Debug.Log("Jump Executed");
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }

}
