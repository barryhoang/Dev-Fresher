using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TungTran.Player.Ability
{
    public class AbilitySelectorButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _applyCount;
        [field: SerializeField] public Button Button { get; private set; }

        public void Init(string description, int applyCount)
        {
            _description.text = description;
            _applyCount.text = $"Applied {applyCount} times";
            Button.onClick.RemoveAllListeners();
        }
    }
}
