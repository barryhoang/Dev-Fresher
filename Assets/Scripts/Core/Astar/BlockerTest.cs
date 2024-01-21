using System;
using UnityEngine;
using System.Collections;
using Pathfinding;

[HelpURL("https://arongranberg.com/astar/documentation/stable/class_blocker_test.php")]
public class BlockerTest : MonoBehaviour {
    private SingleNodeBlocker _blocker;

    public void Start ()
    {
        _blocker = GetComponent<SingleNodeBlocker>();
    }

    private void Update()
    {
        _blocker.BlockAtCurrentPosition();
    }
}
