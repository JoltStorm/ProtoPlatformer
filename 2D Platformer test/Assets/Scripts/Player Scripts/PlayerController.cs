using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: 

public class playerController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerEyes;
    public GameObject GameManagerObject;
    Rigidbody2D rb;
    
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel on the player.
    public Vector2 PlayerMoveSpeed;
    public Vector2 PlayerJumpHeight;
    public Vector2 FallSpeed;
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

        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded == false)
        {
            rb.AddForce(FallSpeed, ForceMode2D.Impulse);
        }

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
        //for some reason, when the player isn't fully on the ground it doesn't count as grounded

        //when a collision is detected, set IsGrounded to true. this method makes walljumps possible,
        //But we'll need to change it soon so the player can't just gain infinite height with walljumps.
        //maybe rb.addForce to the opposite direction of the wall?
        //we can just keep the current walljump system too,
        //I guess it depends on what direction we want to take the platforming in. -JS :}


        if (collision.gameObject.CompareTag("spike"))
        {
            PlayerKill();
        }
        if (collision.gameObject.CompareTag("exit"))
        {
            PlayerFinish();
        }
        if (collision.gameObject.CompareTag("touchKill"))
        {
            PlayerKill();
        }
    }

    void PlayerKill()
    {
        Debug.Log("The player is dead!");
        GameManagerObject.GetComponent<GameManager>().DeadOrAlive = false;
        GameManagerObject.GetComponent<GameManager>().isFinishScreenActive = true;
        gameObject.SetActive(false);
        playerEyes.SetActive(false);
    }
    //add life or checkpoint system later?

    void PlayerFinish()
    {
        Debug.Log("circle touched (real)");
        GameManagerObject.GetComponent<GameManager>().DeadOrAlive = true;
        GameManagerObject.GetComponent<GameManager>().isFinishScreenActive = true;
        gameObject.SetActive(false);
        playerEyes.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }

}
