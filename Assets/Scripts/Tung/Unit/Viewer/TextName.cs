using System;
using TMPro;
using UnityEngine;

namespace Tung
{
    public class TextName : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textName;
        private Color[] _color = {Color.yellow, Color.blue, Color.red, Color.magenta,Color.white, };
        private static int _name = 0;
        private void Awake()
        {
            _name++;
            _textName.text = "P" + _name;
            _textName.color = _color[_name - 1];
        }

        private void OnDestroy()
        {
            _name = 0;
        }
    }
}
