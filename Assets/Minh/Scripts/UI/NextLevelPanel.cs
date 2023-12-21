using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class NextLevelPanel : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _nextLevel; 
        public void SpawnNextLevel()
        {
            _nextLevel.Raise();
        }
    }
}

