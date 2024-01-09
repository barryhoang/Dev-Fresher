using TMPro;
using UnityEngine;

namespace Tung
{
    public class TriggerButtonView : MonoBehaviour
    {
        public Color[] colors;

        public void ChangeColorWhite(TextMeshProUGUI text)
        {
            text.color = Color.white;
        }
        public void ChangeColorCyan(TextMeshProUGUI text)
        {
            text.color = Color.cyan;
        }
        public void ChangeColorBlack(TextMeshProUGUI text)
        {
            text.color = Color.black;
        }
    }
}
