using System;
using MEC;
using UnityEngine;

namespace Minh
{
    public class Enemy : Hero
    {
        public override void Start()
        {
            _soapListEnemy.Add(this);
            base.Start();
        }
        public void OnDestroy()
        {
             _soapListEnemy.Remove(this);
        }
    }
}