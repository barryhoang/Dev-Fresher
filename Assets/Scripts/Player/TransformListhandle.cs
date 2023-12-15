using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class TransformListhandle : MonoBehaviour
{
    [SerializeField] private ScriptableListTransform _listTransform;

    private void Start()
    {
        _listTransform.Add(this.transform);
        
    }

    private void OnDestroy()
    {
        _listTransform.Remove(this.transform);
    }
}
