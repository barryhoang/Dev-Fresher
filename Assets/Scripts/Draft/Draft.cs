using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using Unity.Mathematics;
using UnityEngine;

namespace Tung
{
    public class Draft : MonoBehaviour
    {
//        [SerializeField] private TweenSettings<Vector3> rotationTweenSettings;
//
//        private void Start()
//        {
//            Timing.RunCoroutine(RotationWeapon().CancelWith(gameObject));
//        }
//        public IEnumerator<float> RotationWeapon()
//        {
//            while (true)
//            {
//               
//                var difference = GameObject.transform.position - transform.position;
//                difference.Normalize();
//                var rotaionZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
//                transform.rotation = Quaternion.Euler(0f, 0f, rotaionZ);
//                var rotation = transform.rotation;
//                rotationTweenSettings.endValue = new Vector3(0,rotation.y,rotation.z - 90);
//                Tween.Rotation(transform, rotationTweenSettings).OnComplete(()=>Tween.Rotation(transform, Quaternion.Euler(0f, 0f, rotaionZ),0.25f));
//                ;
//                yield return Timing.WaitForSeconds(attackSpeed);
//                
//            }
//        }
    }
}
