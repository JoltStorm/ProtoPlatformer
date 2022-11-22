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
    public bool GamePaused = false;
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

        if (Input.GetKeyDown(KeyCode.Alpha1) && GamePaused == false)
        {
            PauseGame();
            GamePaused = true;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && GamePaused == true)
        {
            ResumeGame();
            GamePaused = false;
        }
        //1 and 2 are used (for now) so that pausing doesn't immidiately unpause after pausing. Try to find a fix for this soon. -JS

        if (isFinishScreenActive == true && DeadOrAlive == false)
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

    public void RestartLevel1()
    {
        SceneManager.LoadScene("level1");
        //TEMPORARY. PLEASE REPLACE WITH CURRENT SCENE SOON!!! 
        isFinishScreenActive = false;
    }

    public void RestartLevel2()
    {
        //TEMPORARY. PLEASE REPLACE WITH CURRENT SCENE SOON!!! 
        SceneManager.LoadScene("level2");
    }

    public void GoToDevLevel()
    {
        SceneManager.LoadScene("level0");
        isFinishScreenActive = false;
    }

    public void GoToLevel2()
    {
        //TEMPORARY. PLEASE DELETE AFTER GoToNextLevel IS FINISHED!!!
        SceneManager.LoadScene("level2");
    }

    public void GoToNextLevel()
    {
        //add float storing player's current level,
        //add cases for each float corresponding to each level
    }

}
