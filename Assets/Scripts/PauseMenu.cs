using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);   
    }

    public void pauseMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void returnGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
