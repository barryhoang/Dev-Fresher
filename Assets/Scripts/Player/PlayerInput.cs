using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Vector3Variable _moveInput;
    [SerializeField] private Vector3Variable _posPlayer;

    // Update is called once per frame
    void Update()
    {
        _posPlayer.Value = transform.position;
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
       _moveInput.Value = new Vector3(x,0,z);       
    }
}
