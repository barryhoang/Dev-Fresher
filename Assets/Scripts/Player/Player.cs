using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private ScriptableListGameObject _scriptableListPlayer;
    
    private void Start()
    {
        _scriptableListPlayer.Add(gameObject);
    }
}
