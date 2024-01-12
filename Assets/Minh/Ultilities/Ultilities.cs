using System;
using UnityEngine;

public static class Ultilities 
    {
        public static Vector2Int ToV2Int(this Vector2 source)
        {
            return new Vector2Int(Mathf.RoundToInt(source.x),Mathf.RoundToInt(source.y));
        }
        public static Vector2Int ToV2Int(this Vector3 source)
        {
            return new Vector2Int(Mathf.RoundToInt(source.x),Mathf.RoundToInt(source.y));
        }
    }

