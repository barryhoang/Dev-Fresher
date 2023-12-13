
using Obvious.Soap;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;

    [SerializeField] private ScriptableEventInt _onHitPlayer;
    [SerializeField] private ScriptableEventInt _onPlayerHealthing;
    [SerializeField] private ScriptableEventNoParam _onPlayerDeath;
    
    private void Start()
    {
       
        _currentHealth.Value = _maxHealth.Value;
        _currentHealth.MinMax = new Vector2(0,_maxHealth.Value);
        _currentHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _currentHealth.OnValueChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(float value)
    {
        var temp = value - _currentHealth.PreviousValue;
        if (temp < 0)
        {
            if (_currentHealth <= 0)
            {
                _onPlayerDeath.Raise();
            }
            else
            {
                _onHitPlayer.Raise(Mathf.Abs(Mathf.RoundToInt(temp))); 
            }
        }
        else
        {
            _onPlayerHealthing.Raise(Mathf.Abs(Mathf.RoundToInt(temp)));
        }
    }

    public void TakeDame(float Damage)
    {
        _currentHealth.Add(-Damage);
    }
}
