using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menius : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject PauseMenuUi;
    public GameObject EndGameUI;
    public GameObject ObjectiveMenuUI;

    public static bool GameIsStopped = false;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown("m")) 
        {
            if(GameIsStopped) 
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ShowObjects();
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }


    public void ShowObjects()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void removeObjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = true;
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped=false;
    }

    public void Restart()
    {
        
    }
    public void LoadMenu()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }


    void pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }
}
