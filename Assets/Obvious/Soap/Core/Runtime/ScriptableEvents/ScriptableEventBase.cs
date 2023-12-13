namespace Obvious.Soap
{
    [System.Serializable]
    public abstract class ScriptableEventBase : ScriptableBase
    {
        public virtual System.Type GetGenericType { get; }
    }
}