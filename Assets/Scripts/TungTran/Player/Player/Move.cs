using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _input;
        [SerializeField] private FloatVariable _moveSpeed;
        [SerializeField] private float knockbackSpeed = 10f;
        private Rigidbody rb;
        private bool isMove;
 
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            isMove = true;
        }
    
        void Update()
        {
            if(isMove)
                rb.velocity = _input.Value * _moveSpeed;
        }

        public void KnockBack(Vector3 pos)
        {
            var dir = pos - transform.position;
            dir.Normalize();
            Timing.RunCoroutine(KnockBackTimer(-dir).CancelWith(gameObject));
        }

        public IEnumerator<float> KnockBackTimer(Vector3 dir)
        {
            isMove = false;
            rb.AddForce(dir*knockbackSpeed,ForceMode.Impulse);
            yield return Timing.WaitForSeconds(0.25f);
            isMove = true;
            yield return 0;
        }    
    }
}
