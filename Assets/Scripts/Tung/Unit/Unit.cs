using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        public ScriptableEventPathNodes _eventPathNodes;
        public UnitRenderData unitRenderData;
        public float _speed;
        private void OnEnable()
        {
            _eventPathNodes.OnRaised += Move;
        }

        private void Move(PathNode pathNode)
        {
            var posTarget = new Vector2Int(pathNode.xPos,pathNode.yPos);
            transform.position = Vector2.MoveTowards(transform.position, posTarget,_speed*Time.deltaTime);
        }

        public void Idle()
        {
            
        }

        public void Move()
        {
            
        }
    }
}
