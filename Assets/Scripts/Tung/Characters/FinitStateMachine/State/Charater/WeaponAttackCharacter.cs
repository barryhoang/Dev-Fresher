using UnityEngine;

namespace Tung
{
    public class WeaponAttackCharacter : AttackWeaponState
    {
        private Character _character;
        private bool isBackMoving;
        public WeaponAttackCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName,Character character) : base(entity, stateMachine, animationName)
        {
            _character = character;
        }

        public override void DoCheck()
        {
            base.DoCheck();
            isBackMoving = CheckIsMoveBack();
        }

        public override void Enter()
        {
            // var position =  - 
            // // entity.ShouldFlip(position);
            _character.SetWeaponAttack();
            entity._animatorController.SetDir(DirectionAttack(_character.EnemyWork.transform.position));
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            // if (isBackMoving)
            // {
            //     _character.StateMachine.ChangeState(_character.MoveCharacter);
            // }
        }

        private bool CheckIsMoveBack()
        {
            var distance = Vector2.Distance(_character.transform.position,
                _character.EnemyWork.posAttack[_character.indexWork].position);
            return distance > 0.5f;
        }
        public override void Exit()
        {
            base.Exit();
            _character.SetWeaponAttack();
        }
    }
}
