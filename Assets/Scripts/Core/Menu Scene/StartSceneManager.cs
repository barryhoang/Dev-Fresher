using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Transform nameGame;
    [SerializeField] private Transform goToPosition;

    private void Start()
    {
        Tween.Scale(nameGame, 1, duration: 1);
        Tween.PositionY(nameGame, goToPosition.transform.position.y, 1);
    }
}
