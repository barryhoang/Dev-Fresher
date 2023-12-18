
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    
    public class Character : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _fight;
        public float speed;
        private bool isAttack = false;

        public GameObject target;
        public void Fighting()
        {
            
        }

        private IEnumerator<float> Move()
        {
            while (true)
            {
                var position = transform.position;
                var dir = target.transform.position - position;
                dir.Normalize();

                position += dir * (speed * Time.deltaTime);
                transform.position = position;
                if (Vector3.Distance(target.transform.position, transform.position) <= 1f)
                {
                    Timing.RunCoroutine(Attack().CancelWith(gameObject));
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        public bool CheckMove()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= 1f;
        }
        private IEnumerator<float> Attack()
        {
            Debug.Log("Abc");
            yield return Timing.WaitForOneFrame;
        }
    }
}