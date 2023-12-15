using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class TransformListhandle : MonoBehaviour
{
    [SerializeField] private ScriptableListTransform _listTransform;

    private void Start()
    {
        _listTransform.Add(transform);
    }
    private void Update()
    {
        _listTransform.Remove(transform);
    }
}
