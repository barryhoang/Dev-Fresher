using System;
using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private FloatVariable _experience;
        [SerializeField] private IntVariable _numberRoll;

        public void UpLevel()
        {
            _playerData.Level.Value++;
            _numberRoll.Value++;
            _experience.Reset();
            _playerData.InitiateExp.Value = _playerData.InitiateExp*1.2f;
        }

        private void ResetStatPlayer()
        {
            
        }
    }
}
