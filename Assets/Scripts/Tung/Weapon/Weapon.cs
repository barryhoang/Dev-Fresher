using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private TweenSettings<Vector3> rotationTweenSettings;
        private float _rotationWorkZ;
        public bool _isAttacking;
        public GameObject GameObject;
        public float attackSpeed;
        public AnimationController AnimationController;
        private void Start()
        {
            Timing.RunCoroutine(RotationWeapon().CancelWith(gameObject),"Weapon");
        }
        private IEnumerator<float> RotationWeapon()
        {
            while (true)
            {
                RotateOfEnemy();
                if (_isAttacking)
                {
                    RotateAttackEnemy();
                }
                yield return Timing.WaitForSeconds(attackSpeed);
            }
        }
        private void RotateOfEnemy()
        {
            var difference = GameObject.transform.position - transform.position;
            difference.Normalize();
            _rotationWorkZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotationWorkZ);
        }

        private void RotateAttackEnemy()
        {
            var rotation = transform.rotation;
            AnimationController.SetAnimator(NameAnimation.ATTACK,true);
            rotationTweenSettings.endValue = new Vector3(0,rotation.y,rotation.z - 90);
            Tween.Rotation(transform, rotationTweenSettings).OnComplete(()=>Tween.Rotation(transform, Quaternion.Euler(0f, 0f, _rotationWorkZ),0.25f));
        }
        
        
        
    }
}
