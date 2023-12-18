using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TMPro;
using UnityEngine;
 
namespace Wave
{
    public class CountDown : MonoBehaviour
    {
        public TextMeshProUGUI textWave;
        public TextMeshProUGUI textTime;
        private int _currentWave = 0;
        private Spawn _spawn;
        [SerializeField]  private int _timeInWave = 15;
        [SerializeField] private ScriptableEventBool _stop;
        [SerializeField] private ScriptableEventBool _play;

        private int _timeWork;
        private void Start()
        {
            _spawn = GetComponent<Spawn>();
            _play.Raise(true);
            
        }
        public void StartCountdownTimer()
        {
            Timing.RunCoroutine(CountdownTimer().CancelWith(gameObject));
        }
        
        private IEnumerator<float> CountdownTimer()
        {
            textWave.text = "Wave: " +  _currentWave++;
            _timeWork = _timeInWave;
            while (_timeWork > 0)
            {
                textTime.text = _timeWork.ToString();
                yield return Timing.WaitForSeconds(1f);
                _timeWork--;
                textTime.text = _timeWork.ToString();
            }
            _stop.Raise(true);
            _timeWork = 0;
            Timing.KillCoroutines(_spawn.Spawning());
            textWave.text = "Wave: " +  _currentWave++;
        }
    }
}
