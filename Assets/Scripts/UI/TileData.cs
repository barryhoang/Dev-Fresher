using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    public bool _isHeroSpawnTile;
}
