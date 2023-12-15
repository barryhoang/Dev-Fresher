using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class AbilitySeletor : MonoBehaviour
{
    [SerializeField] private AbilityData[] _abilityDatas;
    [SerializeField] private AbilitySeletorButton[] _abilitySelectorButton;
    [SerializeField] private FloatVariable _experience;
    [SerializeField] private FloatVariable _maxExperience;

    private void Awake()
    {
        Array.ForEach(_abilityDatas, x => x.Reset());
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
        DisplayRandomAbilities();
    }
    private void DisplayRandomAbilities()
    {
        var abilitities = new List<AbilityData>(_abilityDatas);
        foreach (var item in _abilitySelectorButton)
        {
            var ability = abilitities[UnityEngine.Random.Range(0, abilitities.Count)];
            item.Init(ability.GetDescription(), ability.ApplyCount);
            item.Button.onClick.AddListener(() =>
            {
                ability.Apply();
                _experience.Reset();
                _maxExperience.Value *= 1.2f;
                gameObject.SetActive(false);
            });
            abilitities.Remove(ability);
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;

    }
}
