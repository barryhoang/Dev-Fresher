using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class TransformListHandler : MonoBehaviour
{
    [SerializeField] private ScriptableListTransform _scriptableListTransform;
    // Start is called before the first frame update
    void Start()
    {
        _scriptableListTransform.Add(transform);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _scriptableListTransform.Remove(transform);
    }
}
