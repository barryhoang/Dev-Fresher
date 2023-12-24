using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace New_Folder_1
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
                    _tweenSettings.endValue += transform.position.x + 0.2f;
                    Tween.PositionX(transform,transform.position.x+ 0.2f,0.1f).OnComplete(
                        ()=> Tween.PositionX(transform,transform.position.x - 0.2f,0.1f));
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

        private IEnumerator<float> AttackAnimation()
        {
            dir.Normalize();
            
            Tween.PositionX(transform,_tweenSettings);
            yield return Timing.WaitForSeconds(0.3f);
            _tweenSettings.endValue += transform.position.x - 0.2f;
            Tween.PositionX(transform,_tweenSettings);
        }
        
        
    }
}

