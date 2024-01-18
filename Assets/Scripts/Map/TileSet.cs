using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [CreateAssetMenu(menuName = "Tile Set")]
    public class TileSet : ScriptableObject
    {
        public List<TileBase> tiles;
    }
}