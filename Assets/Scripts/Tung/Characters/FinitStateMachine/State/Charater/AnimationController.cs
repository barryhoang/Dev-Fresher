using System;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private TweenSettings tweenSettings;
        [SerializeField] private float endValueAttack;
        private Vector2 _direction;
        public Animator _animator;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetDir(Vector2 dir)
        {
            _direction = dir;
        }
        public void SetAnimator(NameAnimation nameAni ,bool value)
        {
            switch (nameAni)
            {
                case NameAnimation.IDLE:
                    _animator.SetBool("Idle",value);
                    return;
                case NameAnimation.MOVE:
                    _animator.SetBool("Move",value);
                    return;
                case NameAnimation.ATTACK:
                    _animator.SetBool("Idle",value);
                    Tween.Position(transform, (Vector2) transform.position +_direction* endValueAttack,tweenSettings).OnComplete(
                        ()=>
                        Tween.Position(transform,(Vector2) transform.position - _direction* endValueAttack,tweenSettings));
                    return;
                case NameAnimation.HURT:
                    // Tween.PositionX(transform, transform.position.x - endValueAttack,tweenSettings).OnComplete(
                    //     ()=>
                    //         Tween.PositionX(transform,transform.position.x + endValueAttack,tweenSettings));
                    _animator.SetTrigger("Hit");
                    Tween.PositionY(transform, transform.position.y + endValueAttack,tweenSettings).OnComplete(
                        ()=>
                            Tween.PositionY(transform,transform.position.y - endValueAttack,tweenSettings));
                    return;
                case NameAnimation.DEATH:
                    _animator.SetTrigger("Death");
                    return;
                default:
                    Debug.Log("No Name Animation");
                    return;
            }   
          
        }

        public void AnimationAttack()
        {
            
        }
    }
}

public enum NameAnimation
{
    NONE,
    IDLE,
    MOVE,
    ATTACK,
    HURT,
    DEATH,
}