using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class DungeonLevelViewer : MonoBehaviour
    {
        [SerializeField] private IntVariable _timeRound;
        private void OnEnable()
        {
            Timing.RunCoroutine(CountDown().CancelWith(gameObject));
        }

        private void OnDisable()
        {
            _timeRound.ResetToInitialValue();
        }

        private IEnumerator<float> CountDown()
        {
            while (_timeRound.Value > 0)
            {
                yield return Timing.WaitForSeconds(1f);
                _timeRound.Value--;
            }
        }
    }
}