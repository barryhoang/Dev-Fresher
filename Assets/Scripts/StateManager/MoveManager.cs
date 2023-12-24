using Entity;
using UnityEngine;

namespace StateManager
{
    public class MoveManager : MonoBehaviour
    {
        public static MoveManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public void Move(ScriptableListCharacter character ,ScriptableListEnemy enemy)
        {
            foreach (var entity in character)
            {
                var closest = enemy.GetClosest(entity.transform.position);
                entity.Move(closest);
            }
            foreach (var entity in enemy)
            {
                var closest = character.GetClosest(entity.transform.position);
                entity.Move(closest);
            }   
        }
    }
}
