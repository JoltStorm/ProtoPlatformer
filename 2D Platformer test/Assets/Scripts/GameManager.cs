using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object References")]
    public GameObject finishScreenAlive;
    public GameObject finishScreenDead;
    public GameObject player;
    public GameObject playerEyes;
    public GameObject torus;

    [Header("Bools")]
    public bool isFinishScreenActive = false;
    public bool DeadOrAlive = true;
    //false = dead, true = alive
    public bool GamePaused = false;

    [Header("Current Level Vars")]
    public float CurrentLevelNum = 1;
    public string CurrentLevel;

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(player.activeInHierarchy == false)
            {
                ExternalRespawn();
            }

        }

        //1 and 2 are used (for now) so that pausing doesn't immidiately unpause after pausing. Try to find a fix for this soon. -JS
        if (player.activeInHierarchy == false)
        {
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
        else
        {
            finishScreenDead.SetActive(false);
            finishScreenAlive.SetActive(false);
        }

        if (DeadOrAlive && isFinishScreenActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToNextLevel();
            }
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

    public void ExternalRespawn()
    {
        player.SetActive(true);
        playerEyes.SetActive(true);
        player.GetComponent<Transform>().position = player.GetComponent<PlayerController>().spawnPos;
        print("player has been respawned externally");
        torus.SetActive(true);
    }

    public void GoToNextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        CurrentLevelNum = float.Parse(sceneName.Substring(5, sceneName.Length - 5)) + 1;
        CurrentLevel = "level" + CurrentLevelNum;
        
        SceneManager.LoadScene(CurrentLevel);
        //thanks Enckripted

    }

    public void RestartLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        CurrentLevelNum = float.Parse(sceneName.Substring(5, sceneName.Length - 5));
        CurrentLevel = "level" + CurrentLevelNum;

        SceneManager.LoadScene(CurrentLevel);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("level1");
        
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("level2");
    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene("level3");
    }

    public void GoToLevel4()
    {
        SceneManager.LoadScene("level4");
    }

    public void GoToLevel5()
    {
        SceneManager.LoadScene("level5");
    }
    
    public void GoToLevel6()
    {
        SceneManager.LoadScene("level6");
    }

}
