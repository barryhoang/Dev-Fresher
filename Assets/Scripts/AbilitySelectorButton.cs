using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectorButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _applyCount;

    public Button Button;

    public void Init(string description, int applyCount)
    {
        applyCount = 1;
        _description.text = description;
        _applyCount.text = $"Applied {applyCount} time";
    }
}
