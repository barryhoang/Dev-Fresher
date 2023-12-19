using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Hero1 : BaseHero
{
    [SerializeField] private bool IsMoving;
    [SerializeField] private float _timeToMove = 0.2f;
    public GameObject _heroTransform;
    public GameObject _enemyTransform;


    private void Awake()
    {
        //Timing.RunCoroutine(_moveHero());
    }

    private IEnumerator<float> _moveHero()
    {
        IsMoving = true;
        float _elapsedTime = 0;
        Vector2 _heroPos = _heroTransform.transform.position;
        Vector2 _enemyPos = _enemyTransform.transform.position;
        Vector2 direction = _heroPos - _enemyPos;
        while (_elapsedTime<_timeToMove)
        {
            transform.position = Vector2.Lerp(_heroPos, direction, _elapsedTime/_timeToMove);
            _elapsedTime += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }

        IsMoving = false;
    }
}
