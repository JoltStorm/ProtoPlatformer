using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //TODO: Change floor tiles so they work properly (no more ceiling bumps)


    [Header("Dev Tools")] // for cheats and testing tools
    //find a silly secret way to enable these >:}
    // ! set the actually useful ones to public for testing, set all the funny cheat ones to private since we won't need them in testing
    


    [Header("Object References")]
    public GameObject player;
    public GameObject playerEyes;
    private Rigidbody2D rb;

    public GameObject GameManagerObject;

    public GameObject Torus;
    public GameObject finishDead;

    [Header("Vectors")]
    //I made these public for easy movement speed/jump height tweaks, edit the values through the inspect panel on the player.
    //default values are next to the vectors
    public Vector2 FallSpeed; //X=0 Y= -160

    public Vector2 VspringForce; //X=0 Y=200
    public Vector2 HLspringForce; //X= -200 Y=0

    public Vector2 CurrentCheckpointLocation = new(0, 5); //set this to last touched checkpoint


    [Header("Floats")]
    public float PlayerMoveSpeed;
//  public float SpeedCapX; default is 100
    public float SpeedCapY; //default is 100
    public float dirX;
    public float playerJumpHeight; //default is 80
    public float HRspringForce;

    public float CoyoteTime;
    private float CoyoteTimeCounter;


    [Header("Bools")]
    public bool IsGrounded = true;
    public bool DoubleDepleted = false;
    public bool SpeedcapEnabled = true;
    public bool bounceFloorTouched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //for easy referencing to the player's rigidbody
    }

    
    void Update()
    {
        //revamped movement system

        dirX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(dirX * PlayerMoveSpeed, rb.velocity.y);

        if (IsGrounded == true)
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && CoyoteTimeCounter > 0f)
        {
            //rb.AddForce(PlayerJumpHeight, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight);
        }

        if (Input.GetButtonUp("Jump") && CoyoteTimeCounter > 0f)
        {
            CoyoteTimeCounter = 0f;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded == false && DoubleDepleted == false)
        {
            rb.velocity = new Vector2(0, 0);
            //velocity is reset as to allow more horizontal movement in midair
            //rb.AddForce(playerJumpHeight, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight);
            DoubleDepleted = true;
        }

        SpeedcapEnabled = true;

        if (rb.velocity.y >= SpeedCapY && SpeedcapEnabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 100);
        }
        if (rb.velocity.y <= -SpeedCapY && SpeedcapEnabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -100);
        }

        //The velocity caps are for wallboosting and bounceFloors

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
        transform.position = CurrentCheckpointLocation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            IsGrounded = true;
        }

        if(collision.gameObject.CompareTag("wall"))
        {
            DoubleDepleted = false;
        }

        if(collision.gameObject.CompareTag("ceiling"))
        {
            DoubleDepleted = true;
            IsGrounded = false;
        }

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
            rb.AddForce(new Vector2(HRspringForce, 20), ForceMode2D.Impulse);
        }


        if (collision.gameObject.CompareTag("checkpoint"))
        {
            CurrentCheckpointLocation = collision.transform.position;
            collision.gameObject.SetActive(false);
            print("checkpoint collected");
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
        if (collision.gameObject.CompareTag("floor"))
        {
            IsGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }
}
