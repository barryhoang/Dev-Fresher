using Player;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Player.Weapon
{
    [CreateAssetMenu(menuName = "AbilityData/ShiedData")]
    public class ShieldAbility : FloatAbilityData
    {
        [SerializeField] private GameObject _shieldPrefab;
        [SerializeField] private TransformVariable _playerTranform;

        public override void Apply()
        {
            if (ApplyCount == 0)
            {
                Instantiate(_shieldPrefab, _playerTranform);
                ApplyCount++;
            }
            else
                base.Apply();

        }
        public override string GetDescription()
        {
            return ApplyCount == 0 ? "Spawn Shield" : base.GetDescription();

        }
    }
}
