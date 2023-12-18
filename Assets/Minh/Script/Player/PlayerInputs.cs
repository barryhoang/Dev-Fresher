using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _inputs;

        private void Start()
        {
            Timing.RunCoroutine(PlayerInput());
        }

        private IEnumerator<float> PlayerInput()
        {
            while (true)
            {
                _inputs.Value = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"))
                    .normalized;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}