using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject escapeMenu;
    private bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TurnOnOrTurnOffMenu();
        }
    }

    public void TurnOnOrTurnOffMenu() {
        isPaused = !isPaused;
        escapeMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void RestartGame() {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void LoadMainMenuScene() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
