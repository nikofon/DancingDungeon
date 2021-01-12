using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader 
{
    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
