using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minh
{
    public class HealthViewer : MonoBehaviour
    {
  
        public GameObject maxHealthBar;
        public GameObject healthBar;
       // public GameObject stickedCharacter;
        private float _health = 0;
        private float _maxHealth = 0;
        private float _healthBarSize1;
        

        public void Start()
        {
        
        }
        public void Update()
        {
        }
        // public void FollowShip()
        // {
        //     transform.position = new Vector3(stickedCharacter.transform.position.x, stickedCharacter.transform.position.y+0.6f, stickedCharacter.transform.position.z);
        // }    
        public void HealthBarSize(float maxHealth, float health)
        {
            _health = health;
            _maxHealth = maxHealth; 
            _healthBarSize1 = _health / _maxHealth;
            if (healthBar != null)
            {
                healthBar.transform.localScale = new Vector3(_healthBarSize1, 0.075f, 1f);
            }
        }
    }
}

