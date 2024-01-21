using Placement;
using UnityEngine;

namespace Units
{
    public class Hero : Unit
    {
        [SerializeField] private PlacementGrid placementGrid;

        private void Awake()
        {
            placementGrid.selectableHeroes.Add(gameObject);
        }
    }
}
