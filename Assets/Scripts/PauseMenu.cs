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
        GameObject[] music = GameObject.FindGameObjectsWithTag("BGM");
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("BGS");
        for (int i = 0; i < music.GetLength(0); i++)
        {
            Destroy(music[i]);
        }
        for (int i = 0; i < bgs.GetLength(0); i++)
        {
            Destroy(bgs[i]);
        }
        SceneManager.LoadScene(0);
    }
}
