using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField]
    private GameObject PausePanel;
    
    void Update() {

    }

    public void Pause() {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue() {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
