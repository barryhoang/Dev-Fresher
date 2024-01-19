using System;
using UnityEngine;
using PrimeTween;

namespace Unit
{
    public class UnitViewer : MonoBehaviour
        //UnitViewer: Đổi hình hero, enemy theo input. Thực hiện tween anim idle, move, attack, die theo event.
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer sprite;

        private BaseUnit _baseUnit;

        private Tween _tween;
        public Vector3 direction;

        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Moving = Animator.StringToHash("Moving");


        private void OnEnable()
        {
            animator.SetBool(Dead, false);
            animator.SetBool(Moving, false);
        }
        
        public void SetFlip(float x)
        {
            sprite.flipX = !(x > 0);
        }

        public void SetAnimation(AnimationState state)
        {
            switch (state)
            {
                case AnimationState.Idle:
                    break;
                case AnimationState.Move:
                    animator.SetBool(Moving,true);
                    break;
                case AnimationState.Attack:
                    animator.SetBool(Moving,false);
                    break;
                case AnimationState.Dead:
                    animator.SetBool(Dead,true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(AnimationState), state, null);
            }
        }
        
        public void StartAttackingAnimation(BaseUnit unit)
        {
            var unitTransform = unit.transform;
            _tween =  Tween.Position(unitTransform, unitTransform.position +  direction * 0.3f, 1f);
        }

        public void EndAttackingAnimation(BaseUnit unit)
        {
            var unitTransform = unit.transform;
            _tween = Tween.Position(unitTransform, unitTransform.position -  direction * 0.3f, 1f);
        }

        private void OnDisable()
        {
            _tween.Stop();
        }
        
        public enum AnimationState
        {
            Idle,
            Move,
            Attack,
            Dead
        }
    }
}
