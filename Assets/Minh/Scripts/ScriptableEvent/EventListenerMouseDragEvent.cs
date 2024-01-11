using UnityEngine;
using UnityEngine.Events;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/EventListeners/EventListenerMouseDragEvent")]
    public class EventListenerMouseDragEvent : EventListenerGeneric<MouseDrag>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<MouseDrag>[] EventResponses => _eventResponses;

        [System.Serializable]
        public class EventResponse : EventResponse<MouseDrag>
        {
            [SerializeField] private ScriptableEventMouseDragEvent _scriptableEvent = null;
            public override ScriptableEvent<MouseDrag> ScriptableEvent => _scriptableEvent;

            [SerializeField] private MouseDragEventUnityEvent _response = null;
            public override UnityEvent<MouseDrag> Response => _response;
        }

        [System.Serializable]
        public class MouseDragEventUnityEvent : UnityEvent<MouseDrag>
        {
            
        }
    }
}