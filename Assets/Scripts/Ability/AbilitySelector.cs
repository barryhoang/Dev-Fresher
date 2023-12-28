using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
   [SerializeField] private AbilityData[] _abilityDatas;
   [SerializeField] private AbilitySelectorButton[] _abilitySelectorButtons;
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
      //select 3 random abilities
      var abilities = new List<AbilityData>(_abilityDatas);
      foreach (var abilitySelectorButton in _abilitySelectorButtons)
      {
         var ability = abilities[UnityEngine.Random.Range(0, abilities.Count)];
         abilitySelectorButton.Init(ability.GetDescription(), ability.ApplyCount);
         abilitySelectorButton.Button.onClick.AddListener(() =>
         {
            ability.Apply();
            _experience.ResetToInitialValue();
            _maxExperience.Value *= 1.2f;
            gameObject.SetActive(false);
         });
         abilities.Remove(ability);
      }
   }

   private void OnDisable()
   {
      Time.timeScale = 1f;
   }
}
