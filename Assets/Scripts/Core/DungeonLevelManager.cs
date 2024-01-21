using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DungeonLevelManager : MonoBehaviour
{
    [SerializeField] private ScriptableVariable<int> currentLevel;
    [SerializeField] private ScriptableEventNoParam win;
    [SerializeField] private ScriptableEventNoParam lose;
    [SerializeField] private ScriptableEventNoParam nextLevel;
    [SerializeField] private ScriptableEventNoParam mainMenu;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void OnEnable()
    {
        win.OnRaised += WinLevel;
        lose.OnRaised += LoseLevel;
        nextLevel.OnRaised += NextLevel;
        mainMenu.OnRaised += goToMainMenu;
    }

    private void goToMainMenu()
    {
        losePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    private void NextLevel()
    {
        winPanel.SetActive(false);
        currentLevel.Value++;
    }

    private void WinLevel()
    {
        winPanel.SetActive(true);
        //currentLevel.Value++;
    }

    private void LoseLevel()
    {
        currentLevel.Value = 1;
        losePanel.SetActive(true);
    }

    private void OnDisable()
    {
        win.OnRaised -= WinLevel;
        lose.OnRaised -= LoseLevel;
        nextLevel.OnRaised -= NextLevel;
        mainMenu.OnRaised -= goToMainMenu;
    }
}
