using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO: 

public class PlayerController : MonoBehaviour
{
    [Header("Object References")]
    public GameObject player;
    public GameObject playerEyes;
    public GameObject GameManagerObject;
    public GameObject Torus;
    private Rigidbody2D rb;
    public GameObject finishDead;

    [Header("Floats")]
    public float SpeedCapX; //unused for now
    public float SpeedCapY; //default is 100

    [Header("Vectors")]
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel on the player.
    //default values are next to the vectors
    public Vector2 PlayerMoveSpeed; //X=50 Y=0
    public Vector2 PlayerJumpHeight; //X=0 Y=80
    public Vector2 FallSpeed; //X=0 Y= -160
    public Vector2 VspringForce; //X=0 Y=200
    public Vector2 HLspringForce; //X= -200 Y=0
    public Vector2 HRspringForce; //X=200 Y=0
    public Vector2 spawnPos = new(0, 5);
    //note: dash removed for now, if it seems like a good enough idea we can add it back later

    [Header("Bools")]
    public bool IsGrounded = true;
    public bool DoubleDepleted = false;
    public bool SpeedcapEnabled = true;
    //public for debugging reasons. 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //for easy referencing to the player's rigidbody.
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-PlayerMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(PlayerMoveSpeed * Time.deltaTime);
        }
        //player controls are in fixed update to prevent differences in speed with different framerates

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded == true || Input.GetKeyDown(KeyCode.W) && IsGrounded == true)
        {
            rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded == false && DoubleDepleted == false || Input.GetKeyDown(KeyCode.W) && IsGrounded == false && DoubleDepleted == false)
        {
            rb.velocity = new Vector2(0, 0);
            //velocity is reset as to allow more horizontal movement in midair
            rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
            DoubleDepleted = true;
        }

        SpeedcapEnabled = true;

        //GetKeyDown is used to prevent infinite jumps.

        if (rb.velocity.y >= SpeedCapY && SpeedcapEnabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 100);
        }
        if (rb.velocity.y <= -SpeedCapY && SpeedcapEnabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -100);
        }
        //The velocity caps are for wallboosting and springs

        if (IsGrounded == true)
        {
            DoubleDepleted = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded == false || Input.GetKeyDown(KeyCode.S) && IsGrounded == false)
        {
            rb.AddForce(FallSpeed, ForceMode2D.Impulse);
        }

        //jump and fastfall are in regular update because jumping gets a little funky when it's in fixed update.
        //experiment with different FPS caps to see if there's any difference soon

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Respawn();
        }
    }



    void Respawn()
    {
        print("respawn ran");
        player.SetActive(true);
        playerEyes.SetActive(true);
        rb.velocity = new Vector2(0, 0);
        transform.position = spawnPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;

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

        if (collision.gameObject.CompareTag("VSpring"))
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(VspringForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("HLspring"))
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(HLspringForce, ForceMode2D.Impulse);
        } 
        if (collision.gameObject.CompareTag("HRspring"))
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(HRspringForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("checkpoint"))
        {
            spawnPos = collision.gameObject.transform.position;
            print("checkpoint collected");
            collision.gameObject.SetActive(false);
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
        GameManagerObject.GetComponent<GameManager>().CurrentLevel += 1;
        Debug.Log("circle touched (real)");
        GameManagerObject.GetComponent<GameManager>().DeadOrAlive = true;
        GameManagerObject.GetComponent<GameManager>().isFinishScreenActive = true;
        player.SetActive(false);
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
