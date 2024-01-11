using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

namespace Tung
{
    [AddComponentMenu("Soap/EventListeners/EventListenerUnit")]
    public class EventListenerUnit : EventListenerGeneric<Unit>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<Unit>[] EventResponses => _eventResponses;

        [System.Serializable]
        public class EventResponse : EventResponse<Unit>
        {
            [SerializeField] private ScriptableEventUnit _scriptableEvent = null;
            public override ScriptableEvent<Unit> ScriptableEvent => _scriptableEvent;

            [SerializeField] private UnitUnityEvent _response = null;
            public override UnityEvent<Unit> Response => _response;
        }

        [System.Serializable]
        public class UnitUnityEvent : UnityEvent<Unit>
        {
            
        }
    }
}