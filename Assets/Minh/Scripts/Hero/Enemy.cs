using System;
using UnityEngine;

namespace Minh
{
    public class Enemy : Hero
    {
        private void Start()
        {
            _soapListEnemy.Add(this);
        }
    }
}