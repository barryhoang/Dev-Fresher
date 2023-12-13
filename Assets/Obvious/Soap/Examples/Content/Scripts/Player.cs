using UnityEngine;
using UnityEngine.Events;

namespace Obvious.Soap.Example
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ScriptableListPlayer scriptableListPlayer = null;
        [SerializeField] private UnityEvent _onNotified = null;

        private void Awake()
        {
            scriptableListPlayer.Add(this);
        }

        private void OnDestroy()
        {
            scriptableListPlayer.Remove(this);
        }

        public void Notify()
        {
            _onNotified?.Invoke();
        }
    }

   
}