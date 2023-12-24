using Obvious.Soap;
using UnityEngine;

namespace Entity
{
    public class Character : Entity
    {
        public ScriptableListCharacter _character;

        protected override void OnEnable()
        {
            base.OnEnable();
            _character.Add(this);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _character.Remove(this);
        }
    }
}
