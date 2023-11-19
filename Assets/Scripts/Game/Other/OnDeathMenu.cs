using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnDeathMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject onDeathMenu;
    [SerializeField]
    private TMP_Text scoreText;
    private ScoreController scoreController;

    void Start() {
        scoreController = FindObjectOfType<ScoreController>();
        if (scoreController == null) {
            Debug.LogError("Nie znaleziono scoreController'a");
        }
    }

    public void OnDeathAnimationEnd() {
        Debug.Log("Wywołanie animacji");
        float score = scoreController.Score;
        Debug.Log("ustawienie score'a: " + score);
        scoreText.text = "Score: " + score;
        onDeathMenu.SetActive(true);
    }

}
