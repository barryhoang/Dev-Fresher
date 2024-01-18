using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Minh
{
    public class HeroViewer : MonoBehaviour
    {
        public Animator _animator;
        private const string _run = "RunState";
        [SerializeField] private GameObject _attackVfxPrefab;
        public void MoveHero(Transform position, Vector3 EndPosition, float Speed)
        {
            Tween.Position(position,EndPosition, Speed);
            _animator.SetFloat(_run ,0.5f);
            //Animation Di Chuyen
        
        }

        public void StopMoving()
        {
            _animator.SetFloat(_run ,0f);
        }

        public IEnumerator<float> HeroAttack(Hero hero,Transform position,Vector3 EndPosition, float attackRate)
        {
            //Ap dung tween tan cong va animation tan cong
            Tween.Position(position, EndPosition, attackRate, Ease.Default, 2,
                CycleMode.Yoyo);
            _animator.SetFloat(_run ,0f);
            Vector2 dir = (hero.transform.position - transform.position).normalized;
           // transform.up = dir.normalized;
            Vector3 myRotationAngles = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles;
            yield return Timing.WaitForSeconds(attackRate/2);
            if (transform == null) yield break;
         Instantiate(_attackVfxPrefab, position.position, Quaternion.LookRotation(Vector3.forward, dir.normalized));
        }
    }

}
