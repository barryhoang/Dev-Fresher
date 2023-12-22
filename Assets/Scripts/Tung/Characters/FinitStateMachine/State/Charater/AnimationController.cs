using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private TweenSettings tweenSettings;
        [SerializeField] private TweenSettings<Vector3> _tweenSettings;
        [SerializeField] private float endValueAttack;
        private Vector2 _direction;
        public Transform target;
        private Tween _tween;
        public Animator _animator;

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
                    Timing.RunCoroutine(AnimtaionAttack().CancelWith(gameObject));
                    return;
                case NameAnimation.HURT:
                    // Tween.PositionX(transform, transform.position.x - endValueAttack,tweenSettings).OnComplete(
                    //     ()=>
                    //         Tween.PositionX(transform,transform.position.x + endValueAttack,tweenSettings));
                    _animator.SetTrigger("Hit");
                    // Tween.PositionY(transform, transform.position.y + endValueAttack,tweenSettings).OnComplete(
                    //     ()=>
                    //         Tween.PositionY(transform,transform.position.y - endValueAttack,tweenSettings));
                    return;
                case NameAnimation.DEATH:
                    _animator.SetTrigger("Death");
                    return;
                default:
                    Debug.Log("No Name Animation");
                    return;
            }
        }
        

        private IEnumerator<float> AnimtaionAttack()
        {
            Tween.Position(transform, (Vector2) transform.position + _direction * endValueAttack,
                tweenSettings);
            yield return Timing.WaitForSeconds(tweenSettings.duration);
            Tween.Position(transform,(Vector2) transform.position - _direction* endValueAttack,tweenSettings);
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