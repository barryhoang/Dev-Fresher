using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Vector3 offset=new Vector3(0,2,0);
    private Vector3 RandomizeIntensity=new Vector3(0.5f,0,0);
    void Start()
    {
        Destroy(gameObject,0.5f);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x,RandomizeIntensity.x),Random.Range(-RandomizeIntensity.y,RandomizeIntensity.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
