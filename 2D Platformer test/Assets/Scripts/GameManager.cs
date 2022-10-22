using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject finishScreen;
    public bool isFinishScreenActive = false;

    // Start is called before the first frame update
    void Start()
    {
        finishScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFinishScreenActive == true)
        {
            finishScreen.SetActive(true);
        }
        else
        {
            finishScreen.SetActive(false);
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
