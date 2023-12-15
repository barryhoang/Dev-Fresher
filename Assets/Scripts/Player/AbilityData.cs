using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [SerializeField] protected string _description;
    public int ApplyCount { get; protected set; }

    public virtual void Apply()
    {
        ApplyCount++;
    }
    public abstract string GetDescription();

    public void Reset()
    {
        ApplyCount = 0;
    }
}
