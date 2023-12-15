using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void Pause()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        obj.GetComponent<PlayerHealth>().ResetHealth();
        Time.timeScale = 1f;
    }
}
