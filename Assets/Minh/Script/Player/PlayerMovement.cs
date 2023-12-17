using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _inputs;
        [SerializeField] private FloatReference _speedMultiplier;
        [SerializeField] private TransformVariable _playerTransform;

        [SerializeField] private float _speed = 5f;

        // Start is called before the first frame update
        private void Awake()
        {
            _playerTransform.Value = transform;
            Debug.Log(_playerTransform.Value + "Player Position");
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += _inputs.Value * _speed * _speedMultiplier * Time.deltaTime;
        }
    }
}