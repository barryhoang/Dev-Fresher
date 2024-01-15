using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using UnityEngine.UI;

public class PlacementInput : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 btnDown;
    [SerializeField] private ScriptableEventVector2 btnDrag;
    [SerializeField] private ScriptableEventVector2 btnUp;
    [SerializeField] private Button fightButton;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        fightButton.gameObject.SetActive(true);
        var button = fightButton.GetComponent<Button>();
        button.onClick.AddListener(StartOnClick);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            btnDown.Raise(GetMousePoint());
        }else if (Input.GetMouseButton(0))
        {
            btnDrag.Raise(GetMousePoint());
        }else if (Input.GetMouseButtonUp(0))
        {
            btnUp.Raise(GetMousePoint());
        }
    }
    
    private static Vector2 GetMousePoint()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void StartOnClick()
    {
        gameManager.currentState = GameManager.State.Fight;
    }
}
