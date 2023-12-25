using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Animation
{
    public enum AnimationName
    {
        None = 0,
        Idle = 1,
        Move = 2,
        Attack = 3,
        Hit = 4,
        Dead = 5,
    }
    
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private TweenSettings<float> _tweenSettings;
        [SerializeField] private Animator _animator;

        public Vector3 dir;
        
        
        
        public void SetAnimation(AnimationName name,bool setTrue)
        {
            switch (name)
            {
                case AnimationName.Idle:
                    _animator.SetBool("Idle",setTrue);
                    return;
                case AnimationName.Move:
                    _animator.SetBool("Move",setTrue);
                    return;
                case AnimationName.Attack:
                    _animator.SetBool("Idle", setTrue);
                    return;
                case AnimationName.Hit:
                    _animator.SetTrigger("Hit");
                    return;
                case AnimationName.Dead:
                    _animator.SetTrigger("Death");
                    return;
                case AnimationName.None:
                    
                    break;
                default:
                    Debug.Log("Not Name Animtion");
                return;
            }
        }

        public IEnumerator<float> AttackAnimation()
        {
            _animator.SetBool("Idle", true);
            Tween.PositionX(transform, transform.position.x + 0.2f, 0.1f);
            yield return Timing.WaitForSeconds(0.1f);
            Tween.PositionX(transform, transform.position.x - 0.2f, 0.1f);
        }
        
        
    }
}

