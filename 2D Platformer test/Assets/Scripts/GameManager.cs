using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
	[Header("Object References")]

	public GameObject finishScreenAlive;
	public GameObject finishScreenDead;

	public GameObject player;
	public GameObject playerEyes;

	public GameObject torus;

	[Header("Bools")]
	public bool isFinishScreenActive;

	public bool DeadOrAlive = true;

	public bool GamePaused;

    public bool timerEnabled;

    [Header("Floats")]

    public float timePassed;



	[Header("Current Level Vars")]

	public float CurrentLevelNum = 1;

	public string CurrentLevel;

	private void Start()
	{
		CurrentLevel = "level" + CurrentLevelNum;
		finishScreenAlive.SetActive(value: false);
		finishScreenDead.SetActive(value: false);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(player.activeInHierarchy == false)
            {
                ExternalRespawn();
            }

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

            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                RestartLevel();
            }
        }

        if(timerEnabled == true)
        {
            timePassed += Time.deltaTime;
        }

    }

	private void PauseGame()
	{
		Time.timeScale = 0f;
		Debug.Log("Game Paused!");
	}

	private void ResumeGame()
	{
		Time.timeScale = 1f;
		Debug.Log("Game Resumed!");
	}

	public void ExternalRespawn()
	{
		player.SetActive(true);
		playerEyes.SetActive(true);
		player.transform.position = player.GetComponent<PlayerController>().CurrentCheckpointLocation;
		MonoBehaviour.print("player has been respawned externally");
		torus.SetActive(true);
	}

	public void GoToNextLevel()
	{
		string text = SceneManager.GetActiveScene().name;
		CurrentLevelNum = float.Parse(text.Substring(5, text.Length - 5)) + 1f;
		CurrentLevel = "level" + CurrentLevelNum;
		SceneManager.LoadScene(CurrentLevel);
	}

	public void RestartLevel()
	{
		string text = SceneManager.GetActiveScene().name;
		CurrentLevelNum = float.Parse(text.Substring(5, text.Length - 5));
		CurrentLevel = "level" + CurrentLevelNum;
		SceneManager.LoadScene(CurrentLevel);
	}

    public void RestartGame()
    {
        SceneManager.LoadScene("level1");
        
    }

	public void GoToLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
