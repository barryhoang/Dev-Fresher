using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minh
{
    public class AbilitySelectionButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _applyCount;

        [field: SerializeField] public Button Button { get; set; }


        public void Init(string description, int applyCount)
        {
            _description.text = description;
            _applyCount.text = $"Applied {applyCount} times";
            Button.onClick.RemoveAllListeners();
        }
    }
}