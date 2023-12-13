using UnityEngine;

namespace Obvious.Soap.Editor
{
    public class SoapSettings : ScriptableObject
    {
        [Tooltip("Default: displays all the parameters of variables. Minimal : only displays the value.")]
        public EVariableDisplayMode VariableDisplayMode = EVariableDisplayMode.Default;
    }

    public enum EVariableDisplayMode
    {
        Default,
        Minimal
    }
}