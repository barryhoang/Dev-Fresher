using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Minh
{
    public class ChangingTileSystem : MonoBehaviour
    {
        public static ChangingTileSystem current;
        public GridLayout gridLayout;
        public Tilemap mainTileMap;
        public Tilemap tempTileMap;

        private static Dictionary<TileType, TileBase> _tileBases = new Dictionary<TileType, TileBase>();

        #region Unity Methods
        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            string tilePath = @"Tile\";
            _tileBases.Add(TileType.Empty,null);
            _tileBases.Add(TileType.White,Resources.Load<TileBase>(tilePath+"white"));
            _tileBases.Add(TileType.Red,Resources.Load<TileBase>(tilePath+"red"));
            _tileBases.Add(TileType.Green,Resources.Load<TileBase>(tilePath+"green"));
        }

        private void Update()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Tilemap Management

        

        #endregion

        #region Building Placement

        

        #endregion
        
    }

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red,
    }
}