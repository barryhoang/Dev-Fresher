using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_2 : Spawn
{
    public GameObject GameObject;

    private void Update()
    {
        SpawnInCamra();
    }

    private void SpawnOutCamra()
    {
        
    }

    private void SpawnInCamra()
    {
        Vector3 spawnPosition = Camera.main.transform.position + new Vector3(0, 0, -10);
        GameObject spawnedObject = Instantiate(GameObject, spawnPosition, Quaternion.identity);
    }
}
