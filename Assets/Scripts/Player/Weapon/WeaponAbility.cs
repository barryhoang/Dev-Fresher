using Obvious.Soap;
using UnityEngine;

namespace Player.Weapon
{
    [CreateAssetMenu(menuName = "AbilityData/WeaponAbility")]
    public class WeaponAbility : AbilityData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private TransformVariable _playerTransform;

        private Vector3[] _offSets = new[]{
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back,
            Vector3.left + Vector3.forward,
            Vector3.right + Vector3.back,
            Vector3.forward + Vector3.right,
            Vector3.back + Vector3.left,
        };

        public override void Apply()
        {
            var weapon = Instantiate(_prefab, _playerTransform);
            weapon.transform.localPosition = _offSets[ApplyCount % _offSets.Length];
            base.Apply();
        }

        public override string GetDescription()
        {
            return _description;
        }
    }
}
