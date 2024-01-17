using System;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class UnitViewer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Vector3 dir;

        public void SetAnimation(AniName aniName, bool isActive)
        {
            switch (aniName)
            {
                case AniName.NONE:
                    break;
                case AniName.IDLE:
                    _animator.SetBool("Idle",isActive);
                    break;
                case AniName.MOVE:
                    _animator.SetBool("Move",isActive);
                    
                    break;
                case AniName.ATTACK:
                    _animator.SetBool("Idle",isActive);
                    break;
                case AniName.HIT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(aniName), aniName, null);
            }
        }

        public void SetAttackActive(Unit unit)
        {
            Tween.Position(unit.transform, unit.transform.position +  dir * 0.3f, 0.1f);
        }

        public void SetAttackEnd(Unit unit)
        {
            Tween.Position(unit.transform, unit.transform.position -  dir * 0.3f, 0.1f);
        }

    }

    public enum AniName
    {
        NONE = 0,
        IDLE = 1,
        MOVE = 2,
        ATTACK = 3,
        HIT = 4,
    }
}
