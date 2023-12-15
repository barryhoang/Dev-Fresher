using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData/ShiedData")]
public class ShieldAbility : FloatAbilityData
{
    [SerializeField] private GameObject _ShieldPrefab;
    [SerializeField] private TransformVariable _PlayerTranform;

    public override void Apply()
    {
        if (ApplyCount == 0)
        {
            Instantiate(_ShieldPrefab, _PlayerTranform);
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
