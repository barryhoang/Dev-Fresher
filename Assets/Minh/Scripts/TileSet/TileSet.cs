using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Minh
{
    [CreateAssetMenu(menuName="TileSet")]
    public class TileSet : ScriptableObject
    {
        public List<TileBase> tiles;
    }
}