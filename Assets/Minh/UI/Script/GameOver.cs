using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private FloatVariable _playerHealth;
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField] private ScriptableEventNoParam _onPlayerRevive;
    [SerializeField] protected Button _retryButton;
    [SerializeField] private IntVariable _reviveTime;
    [SerializeField] private TextMeshProUGUI _dieTimeLeft;
    [SerializeField] private TextMeshProUGUI _retryText;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        
    }

    public void Init(string retry, int timeLeft)
    {
        
    }

    private void OnEnable()
    {
        if (_reviveTime > 0)
        {
            _retryText.text = "Retry";
            _retryButton.onClick.AddListener(Retry);
            _dieTimeLeft.text = "You have " + _reviveTime.Value + " more life";
        }
        else
        {
            _dieTimeLeft.text = "You doesn't have more life";
            _retryText.text = "Restart";
        }
       
        Time.timeScale = 0f;
    }

    private void Retry()
    {
        if (_reviveTime > 0)
        {
            
            _onPlayerRevive.Raise();
            OnDisable();
            _playerHealth.Value = _maxHealth;
            gameObject.SetActive(false);
        }
        
    }
}
