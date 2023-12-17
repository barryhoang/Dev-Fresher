using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] protected FloatReference _speed;
        [SerializeField] protected FloatReference _speedMultiply;

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.Die();
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(transform.parent.transform.position, Vector3.up,
                _speed.Value * _speedMultiply * Time.deltaTime);
        }
    }
}