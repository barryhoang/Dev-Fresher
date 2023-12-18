using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Vector3Variable _moveInput;
        [SerializeField] private Vector3Variable _posPlayer;
        [SerializeField] private float knockbackSpeed = 10f;
        private Rigidbody rb;
        private bool isMove;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            isMove = true;
        }
        
        private void Update()
        {
            MoveInput();
            Move();
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

        private void Move()
        {
            if (isMove)
                rb.velocity = _moveInput.Value * _playerData.MoveSpeed;
            _posPlayer.Value = transform.position;
        }
        private void MoveInput()
        {
            var z = Input.GetAxisRaw("Vertical");
            var x = Input.GetAxisRaw("Horizontal");
            _moveInput.Value = new Vector3(x, 0, z);
        }

        private void ResetStatPlayer()
        {
            _playerData.MoveSpeed.Value = 10f;
        }
    }
}