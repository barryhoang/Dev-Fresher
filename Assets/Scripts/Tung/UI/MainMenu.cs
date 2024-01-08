using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button _playButton;
    public Button _exit;

    public void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _exit.onClick.AddListener(OnExitButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButtonClicked()
    {
        // Thoát ứng dụng
        Application.Quit();
    }
}
