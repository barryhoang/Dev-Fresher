
using System;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class CharacterHeath : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Image _image;
        private float _currentHealth;
        public bool isDeath;

        // private void Awake()
        // {
        //     // string temp = "_currentHealth" + IDhealth;
        //     // IDhealth++;
        //     // string path = "Assets/Data/CurrentHealthCharacter/" + temp + ".asset";
        //     //
        //     // if (AssetDatabase.FindAssets(path).Length == 0)
        //     // {
        //     //     _currentHealth = ScriptableObject.CreateInstance<FloatVariable>();
        //     //     AssetDatabase.CreateAsset(_currentHealth, path);
        //     //     AssetDatabase.SaveAssets();
        //     //     AssetDatabase.Refresh();
        //     //     EditorUtility.FocusProjectWindow();
        //     //     Selection.activeObject = _currentHealth;
        //     // }
        // }

        private void Start()
        {
            _characterData.InitialHealh.OnValueChanged += OnMaxHealthChange;
        }

        protected  void OnEnable()
        {
            _currentHealth = _characterData.InitialHealh;
            if (_image != null) 
                _image.fillAmount = _currentHealth / _characterData.InitialHealh.Value;
        }

        private void OnMaxHealthChange(float newValue)
        {
            var diff = newValue - _characterData.InitialHealh.PreviousValue;
            _currentHealth += diff;
        }

        private void OnCurrentHealthChange(float newValue)
        {
             var diff = newValue - _currentHealth;
             if (_image != null) 
                _image.fillAmount = _currentHealth / _characterData.InitialHealh.Value;
            
             if (diff < 0)
                 if (_currentHealth <= 0)
                 {
                     _animationController.SetAnimator(NameAnimation.DEATH, true);
                     transform.parent.gameObject.SetActive(false);
                 }
                 else
                 {
                     _animationController.SetAnimator(NameAnimation.HURT, true);
                 }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth += -damage;
            OnCurrentHealthChange(-damage);
        }
        
        
    }
}
