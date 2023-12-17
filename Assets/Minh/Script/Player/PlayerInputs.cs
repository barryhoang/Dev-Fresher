using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _inputs;

        public void Update()
        {
            _inputs.Value = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        }
    }
}