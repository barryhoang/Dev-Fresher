using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject maxHealthBar;
    public GameObject healthBar;
    public GameObject stickedCharacter;
    private float _health = 0;
    private float _maxHealth = 0;
    private float _healthBarSize1;

    public void Start()
    {
        
    }
    public void Init(GameObject _stickedCharacter)
    {
        stickedCharacter = _stickedCharacter;
        _healthBarSize1 = 1f;
        healthBar.transform.localScale = new Vector3(_healthBarSize1, 1f, 1f);
    }

    public void Update()
    {        
        FollowShip();       
    }
    public void FollowShip()
    {
        transform.position = new Vector3(stickedCharacter.transform.position.x, stickedCharacter.transform.position.y+0.6f, stickedCharacter.transform.position.z);
    }    
    public void HealthBarSize(float maxHealth, float health)
    {
        _health = health;
        _maxHealth = maxHealth; 
        _healthBarSize1 = _health / _maxHealth;
        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(_healthBarSize1, 1f, 1f);
        }
    }
}
