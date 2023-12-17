using UnityEngine;

namespace Minh
{
    public abstract class AbilityData : ScriptableObject
    {
        [SerializeField] protected string _description;
        [SerializeField] public int ApplyCount { get; protected set; }

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
}