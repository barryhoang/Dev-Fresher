using TMPro;
using UnityEngine;

namespace Units
{
    public class PlayerOrder : MonoBehaviour
    {
        [SerializeField] private TextMeshPro orderText;
        private readonly Color[] _color = {Color.yellow, Color.blue, Color.red, Color.magenta,Color.white, };
        private static int _order;
        private void Awake()
        {
            _order++;
            orderText.text = "P" + _order;
            orderText.color = _color[_order - 1];
        }

        private void OnDestroy()
        {
            _order = 0;
        }
    }
}
