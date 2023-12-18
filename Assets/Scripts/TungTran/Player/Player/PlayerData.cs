using System;
using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Health")] [SerializeField] private FloatVariable _initiateHealth;

        [Header("Exp")]
        [SerializeField] private FloatVariable _initiateExp;
        [SerializeField] private IntVariable _level;
        
        [Header("Move")]
        [SerializeField] private FloatVariable _moveSpeed;
        
        public FloatVariable InitiateHealth
        {
            get => _initiateHealth;
        }

        public FloatVariable InitiateExp
        {
            get => _initiateExp;
        }
        
        public IntVariable Level
        {
            get => _level;
        }
        
        public FloatVariable MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        public void ResetStatPlayer()
        {
            _initiateHealth.ResetToInitialValue();
            _initiateExp.ResetToInitialValue();
            _level.ResetToInitialValue();
            _moveSpeed.ResetToInitialValue();
        }
    }
}