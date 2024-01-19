using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using PrimeTween;
using UnityEngine;

namespace Unit
{
    public class Enemy : BaseUnit
    {
        [SerializeField] private ScriptableListEnemy scriptableListEnemy;
        
        private void Awake()
        {
            scriptableListEnemy.Add(this);
        }
    }
}
