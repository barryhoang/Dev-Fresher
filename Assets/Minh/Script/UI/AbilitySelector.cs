using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class AbilitySelector : MonoBehaviour
    {
        [SerializeField] private AbilityData[] _abilityDatas;
        [SerializeField] private AbilitySelectionButton[] _abilitySelectionButtons;
        [SerializeField] private FloatVariable _experience;
        [SerializeField] private FloatVariable _maxExperience;
        [SerializeField] private GameObject _timerGameObject;

        private void Awake()
        {
            for (int i = 0; i < _abilityDatas.Length; i++)
            {
                _abilityDatas[i].Reset();
            }

            //Array.ForEach(_abilityDatas,x=>x.Reset());
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
            DisplayRandomAbilities();
        }

        private void OnDisable() => Time.timeScale = 1f;

        private void DisplayRandomAbilities()
        {
            //select 3 random abilites
            var abilites = new List<AbilityData>(_abilityDatas);
            foreach (var abilitySelectorButton in _abilitySelectionButtons)
            {
                var ability = abilites[UnityEngine.Random.Range(0, abilites.Count)];
                abilitySelectorButton.Init(ability.GetDescription(), ability.ApplyCount);
                abilitySelectorButton.Button.onClick.AddListener(() =>
                {
                    ability.Apply();
                    _experience.Reset();
                    _maxExperience.Value = _maxExperience.Value * 1.2f;
                    gameObject.SetActive(false);
                    _timerGameObject.SetActive(true);
                    Debug.Log("Apply");
                    Debug.Log(_maxExperience.Value + "max experience");
                    Debug.Log(_experience.Value + "Experience value");
                });
                abilites.Remove(ability);
            }
        }
    }
}