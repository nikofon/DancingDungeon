using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject mainMenuGroup;
    private void Start()
    {
        AudioManager.instance.PlayMusic("Menu_Loop");
    }
    public void StartGame()
    {
        LevelLoader.LoadNextLevel();
    }

    public void ChangeHowToPlayState()
    {
        howToPlay.SetActive(!howToPlay.activeSelf);
        mainMenuGroup.SetActive(!mainMenuGroup.activeSelf);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
