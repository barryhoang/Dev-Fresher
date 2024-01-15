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
        private void Start()
        {
            _soapListPlayer.Add(this);
        }
    }
}