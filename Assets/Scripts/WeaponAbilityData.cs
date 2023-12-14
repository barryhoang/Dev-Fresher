using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
[CreateAssetMenu(menuName = "AbilityData/WeaponAbility")]
public class WeaponAbilityData : AbilityData
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private TransformVariable _playerTransform;

    private Vector3[] _offsets = new[]
    {
        Vector3.left,
        Vector3.right,
        Vector3.forward,
        Vector3.back,
        Vector3.left + Vector3.forward,
        Vector3.right + Vector3.back,
        Vector3.back + Vector3.left,
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
