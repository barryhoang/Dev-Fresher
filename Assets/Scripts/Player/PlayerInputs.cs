using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Vector3Variable _inputs;
    
    // Update is called once per frame
    /*void Update()
    {
        _inputs.Value = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
    }*/

    private void Start()
    {
        Timing.RunCoroutine(_playerInput());
    }

    IEnumerator<float> _playerInput()
    {
        while (true)
        {
            _inputs.Value = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            yield return Timing.WaitForOneFrame;
        }
    }
}
