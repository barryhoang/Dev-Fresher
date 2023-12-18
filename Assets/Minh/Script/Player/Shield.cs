using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] protected FloatReference _speed;
        [SerializeField] protected FloatReference _speedMultiply;

        private void Start()
        {
            Timing.RunCoroutine(Rotate());
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.Die();
        }

        private IEnumerator<float> Rotate()
        {
            transform.RotateAround(transform.parent.transform.position, Vector3.up,
                _speed.Value * _speedMultiply * Time.deltaTime);
            yield return Timing.WaitForOneFrame;
        }
    }
}