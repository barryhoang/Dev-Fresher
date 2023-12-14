using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
[CreateAssetMenu(menuName = "AbilityData/FloatAbility")]
public class FloatAbilityData : AbilityData
{
    [SerializeField] private FloatVariable _floatVariable;
    [SerializeField] private FloatReference _increment;
    [SerializeField] private bool isPercent = true;
    [SerializeField] private IncrementOperation _incrementOperation;
    private enum IncrementOperation
    {
        Add, Multiply
    }

    public override void Apply()
    {
        if(_incrementOperation == IncrementOperation.Add)
            _floatVariable.Add(_increment);
        else
        {
            var increment = _increment.Value;
            if (isPercent)
                increment = 1 + increment / 100f;
            _floatVariable.Value *= increment;
        }
        base.Apply();
    }

    public override string GetDescription()
    {
        return string.Format(_description, _increment.Value);
    }
}
