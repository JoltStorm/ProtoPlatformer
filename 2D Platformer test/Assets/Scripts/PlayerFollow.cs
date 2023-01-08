using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public GameObject Player;
    public float zOffset;
    //z offset is -24 for camera, 1 for bg

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + 0, Player.transform.position.y, zOffset);
        //makes the camera move to the player's position, then adds an offset for the Z axis
    }
}