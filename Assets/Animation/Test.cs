using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform pos1;

    public Transform pos2;

    public TweenSettings tweenSettings;
    // Start is called before the first frame update
    void Start()
    {
        Tween.Bezier(transform,transform.position,pos2.position,pos1.position,tweenSettings);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
