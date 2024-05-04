using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject GamesMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);

    }

    public void Exit()
    {
        GamesMenu.SetActive(false);
    }

    public void GameMenuButton()
    {
        GamesMenu.SetActive(true);
    }

    public void PlatformGamesButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void MatchingGameButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
    }

}

