using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Obvious.Soap;
using PrimeTween;

namespace Minh
{
    public class Player : Hero
    {
        public override void Start()
        {
            _soapListPlayer.Add(this);
            base.Start();
        }

        public void OnDestroy()
        {
            _soapListPlayer.Remove(this);
           
        }
    }
}