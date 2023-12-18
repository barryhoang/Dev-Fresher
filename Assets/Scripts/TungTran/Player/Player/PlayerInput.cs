using Obvious.Soap;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _moveInput;
        [SerializeField] private Vector3Variable _posPlayer;
        
        void Update()
        {
            _posPlayer.Value = transform.position;
            var z = Input.GetAxisRaw("Vertical");
            var x = Input.GetAxisRaw("Horizontal");
            _moveInput.Value = new Vector3(x,0,z);       
        }
    }
}
