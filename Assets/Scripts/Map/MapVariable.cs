using UnityEngine;
using Obvious.Soap;

[CreateAssetMenu(fileName = "scriptable_variable_MapVariable.asset", menuName = "Soap/ScriptableVariables/MapVariable")]
public class MapVariable : ScriptableVariable<Hero[,]>
{
    public Vector2Int size;

    public override void Init()
    {
        _value = new Hero[size.x, size.y];
        base.Init();
    }
}