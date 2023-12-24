using Obvious.Soap;
using UnityEngine;

namespace Entity
{
    public class Enemy : Entity
    {
        [SerializeField] private ScriptableListEnemy _listEnemy;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _listEnemy.Add(this);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _listEnemy.Remove(this);
        }
    }
}
