using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object References")]
    public GameObject finishScreenAlive;
    public GameObject finishScreenDead;

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
<<<<<<< Updated upstream
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
=======
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(player.activeInHierarchy == false)
            {
                ExternalRespawn();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExternalRespawn();
        }

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
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
=======
    public void ExternalRespawn()
    {
        player.SetActive(true);
        playerEyes.SetActive(true);
        player.GetComponent<Transform>().position = new Vector2(0, 5);
        print("player has been respawned externally");
        torus.SetActive(true);
    }

>>>>>>> Stashed changes
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

    public void Restart()
    {
        SceneManager.LoadScene("level1");
        
    }
<<<<<<< Updated upstream
    
=======

    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    //select this in the button and type the name of the scene

>>>>>>> Stashed changes
}
