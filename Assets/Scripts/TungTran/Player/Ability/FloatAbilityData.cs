using Obvious.Soap;
using TungTran.Player.Ability;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "AbilityData/FloatAbility")]
    public class FloatAbilityData : AbilityData
    {

        [SerializeField] private FloatVariable _floatVariable;
        [SerializeField] private IncrementOperation _incrementOperation;
        [SerializeField] private FloatReference _increment;
        [SerializeField] private bool _isPrecent = true;
        
        [ContextMenu("Apply")]
        public override void Apply()
        {
            if (_incrementOperation == IncrementOperation.Add)
            {
                _floatVariable.Add(_increment);
            }
            else
            {
                var increment = _increment.Value;
                if (_isPrecent)
                    increment = 1 + increment / 100f;
                _floatVariable.Value *= increment;
            }
            base.Apply();
        }
        public override string GetDescription()
        {
            return string.Format(_description, _increment.Value);
        }
        private enum IncrementOperation
        {
            Add,
            Multiply
        }
    }
}
