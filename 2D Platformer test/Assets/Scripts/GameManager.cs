using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject finishScreenAlive;
    public GameObject finishScreenDead;
    public bool isFinishScreenActive = false;
    public bool DeadOrAlive = true;
    public bool GamePaused = false;
    public float CurrentLevelNum = 1;
    public string CurrentLevel;

    //false = dead, true = alive

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = "level" + CurrentLevelNum;
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

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }

}
