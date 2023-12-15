using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class AbilitySeletor : MonoBehaviour
{
    [SerializeField] private ScriptableEventBool _play;
    [SerializeField] private BoolVariable _isChess; 
    [SerializeField] private IntVariable _numberLevel;
    [SerializeField] private AbilityData[] _abilityDatas;
    [SerializeField] private AbilitySeletorButton[] _abilitySelectorButton;


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
                _numberLevel.Value--;
                if (_isChess)
                {
                    _isChess.Value = false;
                    
                    gameObject.SetActive(false);
                }
                if (_numberLevel.Value <= 0)
                {
                    gameObject.SetActive(false);
                    _play.Raise(true);
                }
                else
                {
                    DisplayRandomAbilities();
                }
               
            });
            abilitities.Remove(ability);
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
