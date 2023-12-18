using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData/ShieldAbility")] 
public class ShieldAbilityData : FloatAbilityData
{
    [SerializeField] private TransformVariable _playerTransform;
    [SerializeField] private Shield _shieldPrefab;

    public override void Apply()
    {
        if (ApplyCount == 0)
        {
            Instantiate<Shield>(_shieldPrefab, _playerTransform);
            ApplyCount++;
        }else base.Apply();
    }

    public override string GetDescription()
    {
        return ApplyCount == 0 ? "Spawn Shield" : base.GetDescription();
    }
}
