using System;
using Obvious.Soap;
using UnityEngine;

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
