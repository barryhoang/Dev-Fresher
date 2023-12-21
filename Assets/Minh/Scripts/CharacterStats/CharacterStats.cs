using System;
using UnityEngine;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;

namespace Minh
{
    public class CharacterStats : MonoBehaviour
    {
        public ScriptableCharacterStats characterStats;

        [SerializeField]

        
        protected virtual void Move(Vector3 target)
        {
            var transform1 = transform;
            var position = transform1.position;
            var direction = (target-transform1.position ).normalized;
            position += direction * characterStats._speed * Time.deltaTime;
            transform1.position = position;
           
        }

       
    }
}
