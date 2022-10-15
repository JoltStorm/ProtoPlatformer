using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: 

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel on the player.
    public Vector2 PlayerMoveSpeed;
    public Vector2 PlayerJumpHeight;
    private Vector2 spawnPos = new(0, 5);

    public bool IsGrounded = true;
    public bool GamePaused = false;
    //public for debugging reasons. 

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //for easy referencing to the player's rigidbody.

    }


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
        }
        //GetKeyDown is used to prevent infinite jumps.

        //player controls

        if (Input.GetKeyDown(KeyCode.Alpha1) && GamePaused == false)
        {
            PauseGame();
            GamePaused = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && GamePaused == true)
        {
            ResumeGame();
            GamePaused = false;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Respawn();
        }

        //1 and 2 are used (for now) so that pausing doesn't immidiately unpause after pausing. Try to find a fix for this soon. -JS

    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Paused!");
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("Game Resumed!");
    }

    void Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = spawnPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        if (collision.gameObject.CompareTag("spike"))
        {
            Debug.Log("ow");
            SceneManager.LoadScene("level0");
        }
    }

    //when a collision is detected, set IsGrounded to true. this method is used to make walljumps possible,
    //But we'll need to change it soon so the player can't just gain infinite height with walljumps.
    //maybe rb.addForce to the opposite direction of the wall?
    //we can just keep the current walljump system too,
    //I guess it depends on what direction we want to take the platforming in. -JS :}

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }

}
