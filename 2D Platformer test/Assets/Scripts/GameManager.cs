// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// GameManager
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
	public bool isFinishScreenActive;

	public bool DeadOrAlive = true;

	public bool GamePaused;

	[Header("Current Level Vars")]
	public float CurrentLevelNum = 1f;

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

            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                RestartLevel();
            }
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
		player.SetActive(value: true);
		playerEyes.SetActive(value: true);
		player.GetComponent<Transform>().position = new Vector2(0f, 5f);
		MonoBehaviour.print("player has been respawned externally");
		torus.SetActive(value: true);
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
	public void RestartGame()
	{
		SceneManager.LoadScene("level1");
	}

	public void GoToLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
