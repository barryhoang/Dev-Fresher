using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData/ShieldAbility")]
public class ShieldAbilityData : FloatAbilityData
{
    [SerializeField] private Shield _shieldPrefab;
    [SerializeField] private TransformVariable _playerTransform;

    public override void Apply()
    {
        if (ApplyCount == 0)
        {
            Instantiate(_shieldPrefab, _playerTransform);
            ApplyCount++;
        }
        else
            base.Apply();
    }

    public override string GetDescription()
    {
        return ApplyCount == 0 ? "SpawnShield" : base.GetDescription();
    }
}
