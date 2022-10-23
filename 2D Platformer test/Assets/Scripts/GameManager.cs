using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject finishScreenAlive;
    public GameObject finishScreenDead;
    public bool isFinishScreenActive = false;
    public bool DeadOrAlive = true;
    //false = dead, true = alive

    // Start is called before the first frame update
    void Start()
    {
        finishScreenAlive.SetActive(false);
        finishScreenDead.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFinishScreenActive == true && DeadOrAlive == false)
        {
            finishScreenDead.SetActive(true);
        }
        else
        {
            finishScreenDead.SetActive(false);
        }

        if (isFinishScreenActive == true && DeadOrAlive == true)
        {
            finishScreenAlive.SetActive(true);
        }
        else
        {
            finishScreenAlive.SetActive(false);
        }
    }

    public void RestartCurentLevel()
    {
        SceneManager.LoadScene("level1");
        //this is temporary, please find a way to have it load the currently open scene
        isFinishScreenActive = false;

    }

    public void GoToDevLevel()
    {
        SceneManager.LoadScene("Level0");
        isFinishScreenActive = false;
    }

}
