using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class HighlightTile : MonoBehaviour
{
    private void Awake()
    {
        Timing.RunCoroutine(Die().CancelWith(gameObject));
    }

    IEnumerator<float> Die()
    {
        yield return Timing.WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    
}
