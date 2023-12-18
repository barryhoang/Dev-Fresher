using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    [CreateAssetMenu(menuName = "AbilityData/WeaponAbility")]
    public class WeaponAbilityData : AbilityData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private TransformVariable _playerTransform;

        private readonly Vector3[] _offsets = new[]
        {
            Vector3.back,
            Vector3.forward,
            Vector3.left,
            Vector3.right,
            Vector3.back + Vector3.left,
            Vector3.forward + Vector3.right,
            Vector3.left + Vector3.forward,
            Vector3.right + Vector3.back,
        };

        public override void Apply()
        {
            var weapon = Instantiate(_prefab, _playerTransform);
            weapon.transform.localPosition = _offsets[ApplyCount % _offsets.Length];
            base.Apply();
        }

        public override string GetDescription()
        {
            return _description;
        }
    }
}