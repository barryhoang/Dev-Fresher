using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _eventfight;
        
        public void OnClickButtonFight()
        {
            _eventfight.Raise();
        }
    }
}
