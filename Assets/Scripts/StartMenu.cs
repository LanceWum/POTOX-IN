using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string LoadScene;
    // public LevelLoader LevelLoader;
    public void Start()
    {
        
    }
    public void LoadNextLevel()
    {
        // LevelLoader.LoadNextLevel();
        SceneManager.LoadScene(LoadScene);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
