using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectorButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _applyCount;
    [field: SerializeField] public Button Button { get; private set; }

    public void Init(string description, int applyCount)
    {
        _description.text = description;
        _applyCount.text = "Applied 1 time";
        Button.onClick.RemoveAllListeners();
    }


}
