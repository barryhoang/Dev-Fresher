using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private FloatVariable _experience;
        [SerializeField] private FloatVariable _maxExperience;
        [SerializeField] private IntVariable _numberRoll;
        [SerializeField] private IntVariable _currentLevel;
    
        public void UpLevel()
        {
            _currentLevel.Value++;
            _numberRoll.Value++;
            _experience.Reset();
            _maxExperience.Value = _maxExperience*1.2f;
        }
    }
}
