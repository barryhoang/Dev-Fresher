using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Vector3Variable _input;
    [SerializeField] private FloatVariable _moveSpeed;
    private Rigidbody rb;
    private bool isMove;
    [SerializeField] private float knockbackSpeed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMove = true;
    }
    
    void Update()
    {
        if(isMove)
            rb.velocity = _input.Value * _moveSpeed;
    }

    public void KnockBack(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        Timing.RunCoroutine(KnockBackTimer(-dir).CancelWith(gameObject));
    }

    public IEnumerator<float> KnockBackTimer(Vector3 dir)
    {
        isMove = false;
        rb.AddForce(dir*knockbackSpeed,ForceMode.Impulse);
        yield return Timing.WaitForSeconds(0.25f);
        isMove = true;
        yield return 0;
    }    
}
