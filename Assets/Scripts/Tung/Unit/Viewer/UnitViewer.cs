using System;
using PrimeTween;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Tung
{
    public class UnitViewer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _avatar;
        public Vector3 dir;
        public float duration = 0.1f;
        public bool flipStart;

        public void SetAnimation(AniName aniName, bool isActive)
        {
            switch (aniName)
            {
                case AniName.NONE:
                    break;
                case AniName.IDLE:
                    _animator.SetBool("Idle", isActive);
                    break;
                case AniName.MOVE:
                    _animator.SetBool("Move", isActive);

                    break;
                case AniName.ATTACK:
                    _animator.SetBool("Idle", isActive);
                    break;
                case AniName.HIT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(aniName), aniName, null);
            }
        }

        public void ResetFlip()
        {
            _avatar.flipX = flipStart;
        }
        public void SetFlip(float x)
        {
            _avatar.flipX = !(x > 0);
        }

        public void SetAttackActive(Unit unit)
        {
            Tween.Position(transform, transform.position + dir * 0.3f, duration);
        }

        public void SetAttackEnd(Unit unit)
        {
            Tween.Position(transform, transform.position - dir * 0.3f, duration);
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
