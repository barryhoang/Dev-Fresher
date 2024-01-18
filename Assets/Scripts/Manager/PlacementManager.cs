using Map;
using UnityEngine;

namespace Manager
{
    public class PlacementManager : MonoBehaviour
    {
        [SerializeField] private MapVariable mapVariable;

        private void Awake()
        {
            mapVariable.Init();
        }
    }
}
