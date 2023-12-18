using System;
using UnityEngine;

namespace UI
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Color _baseColor, _offsetColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;

        void OnMouseEnter()
        {
            _highlight.SetActive(true);
        }

        void OnMouseExit()
        {
            _highlight.SetActive(false);
        }
        
        public void Init(bool isOffset)
        {
            _renderer.color = isOffset ? _offsetColor : _baseColor;
        }

    }
}
