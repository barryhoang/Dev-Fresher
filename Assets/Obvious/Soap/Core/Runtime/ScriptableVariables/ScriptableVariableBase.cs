using UnityEngine;

namespace Obvious.Soap
{
    [System.Serializable]
    public abstract class ScriptableVariableBase : ScriptableBase
    {
        [SerializeField, HideInInspector] private string _guid;

        /// <summary>
        /// Guid is needed to save/load the value to PlayerPrefs.
        /// </summary>
        public string Guid
        {
            get => _guid;
            set => _guid = value;
        }
        
        public virtual System.Type GetGenericType { get; }
    }
}