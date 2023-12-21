
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
        [SerializeField] private FloatVariable _currentHealth;
        // private static int IDhealth;
        public bool isDeath;

        private void Awake()
        {
            // string temp = "_currentHealth" + IDhealth;
            // IDhealth++;
            // string path = "Assets/Data/CurrentHealthCharacter/" + temp + ".asset";
            //
            // if (AssetDatabase.FindAssets(path).Length == 0)
            // {
            //     _currentHealth = ScriptableObject.CreateInstance<FloatVariable>();
            //     AssetDatabase.CreateAsset(_currentHealth, path);
            //     AssetDatabase.SaveAssets();
            //     AssetDatabase.Refresh();
            //     EditorUtility.FocusProjectWindow();
            //     Selection.activeObject = _currentHealth;
            // }
        }

        private void Start()
        {
            _currentHealth.Value = _characterData.InitialHealh;
            _currentHealth.MinMax = new Vector2(0, _characterData.InitialDamage);
            
            _currentHealth.OnValueChanged += OnCurrentHealthChange;
            _characterData.InitialHealh.OnValueChanged += OnMaxHealthChange;
        }

        private void Update()
        {
            if(isDeath)
                transform.parent.gameObject.SetActive(false);
        }

        private void OnMaxHealthChange(float newValue)
        {
            _currentHealth.MinMax = new Vector2(0, newValue);
            var diff = newValue - _characterData.InitialHealh.PreviousValue;
            _currentHealth.Add(diff);
        }

        private void OnCurrentHealthChange(float newValue)
        {
             var diff = newValue - _currentHealth.PreviousValue;
             if (_image != null) 
                _image.fillAmount = _currentHealth.Value / _characterData.InitialHealh.Value;
            
             if (diff < 0)
                 if (_currentHealth <= 0)
                 {
                     _animationController.SetAnimator(NameAnimation.DEATH, true);
                     isDeath = true;
                 }
                 else
                 {
                     _animationController.SetAnimator(NameAnimation.HURT, true);
                 }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Add(-damage);
        }
        
        
    }
}
