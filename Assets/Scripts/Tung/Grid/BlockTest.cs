using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class BlockTest : MonoBehaviour
{
    private SingleNodeBlocker _blocker;
    private void Start()
    {
         _blocker = GetComponent<SingleNodeBlocker>();
    }

    public void Update () {
       

        _blocker.BlockAtCurrentPosition();
    }
}
