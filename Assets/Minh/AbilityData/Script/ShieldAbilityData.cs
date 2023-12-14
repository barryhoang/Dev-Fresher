using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
[CreateAssetMenu(menuName="AbilityData/ShieldAbility")]
public class ShieldAbilityData : FloatAbilityData
{
    [SerializeField] protected Shield _shieldPrefab;

    [SerializeField] protected TransformVariable _playerTransform;
    // Start is called before the first frame update
    public override void Apply()
    {
        if (ApplyCount == 0)
        {
            Instantiate(_shieldPrefab, _playerTransform);
            ApplyCount++;
        }
        else
        {
            base.Apply();
        }
       
    }

    public override string GetDescription()
    {
       return ApplyCount==0?"Spawn Shield":  base.GetDescription();
    }
}
