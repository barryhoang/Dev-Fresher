using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniCharacter : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.SetBool("Idle", true);
    }
}
