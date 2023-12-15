using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    void Start ()
    {
        
    }
  
    
    public void PushOnGameObject(Vector3 forward)
    {
        transform.position += 3 * forward*Time.deltaTime;
    }

    IEnumerator<float> _shout(float time, string text)
    {
        yield return Timing.WaitForSeconds(time);
 
        Debug.Log(text);
    }
}
