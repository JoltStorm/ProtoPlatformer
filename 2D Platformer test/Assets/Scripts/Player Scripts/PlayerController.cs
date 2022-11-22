using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: 

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerEyes;
    public GameObject GameManagerObject;
    public GameObject Torus;
    Rigidbody2D rb;

    
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel on the player.
    public Vector2 PlayerMoveSpeed;
    public Vector2 PlayerJumpHeight;
    public Vector2 FallSpeed;
    public Vector2 DashForce;
    private Vector2 spawnPos = new(0, 5);

    public bool IsGrounded = true;
    public bool DoubleDepleted = false;
    //public for debugging reasons. 

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //for easy referencing to the player's rigidbody.

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-PlayerMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(PlayerMoveSpeed * Time.deltaTime);
        }
        //player controls are in fixed update to prevent differences in speed with different framerates

        if (Input.GetKey(KeyCode.A))
        {

        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded == true)
        {
            rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded == false && DoubleDepleted == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
            DoubleDepleted = true;
        }
        //GetKeyDown is used to prevent infinite jumps.

        if(rb.velocity.y >= 100)
        {
            rb.velocity = new Vector2(rb.velocity.x, 100);
        }

        if(IsGrounded == true)
        {
            DoubleDepleted = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded == false)
        {
            rb.AddForce(FallSpeed, ForceMode2D.Impulse);
        }

        //jump and fastfall are in regular update because jumping gets a little funky when it's in fixed update.


        if (Input.GetKeyDown(KeyCode.Return))
        {
            Respawn();
        }

    }



    void Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = spawnPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        //with our current system, IsGrounded is set to true on any contact. This allows for wall jumping, but the player can also wall grip by
        //moving into the wall. We need to find a way to detect if the player is on a wall, then apply force diagonally away from the wall.
        //This is not a high priority task, so work on this in your own time in a different branch if you want to.


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
        Torus.SetActive(false);
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
