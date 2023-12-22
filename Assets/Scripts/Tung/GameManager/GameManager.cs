using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _finght;
        [SerializeField] private ScriptableEventNoParam _idlefalse;
        [SerializeField] private ScriptableListEnemy _listSoapEnemy;
        [SerializeField] private ScriptableEventNoParam _roundFinish;
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private List<Character> _characters;
        [SerializeField] private IntVariable _timeRound;
        
        public Button button;

        private void Start()
        {
            button.onClick.AddListener(OnButtonClick);
            _idlefalse.OnRaised += StartCountDown;
        }
        

        private void OnDisable()
        {
            _idlefalse.OnRaised -= StartCountDown;
        }

        public void OnButtonClick()
        {
            _finght.Raise();
            _idlefalse.Raise();
        }
        
        
        private void Update()
        {
            if(_listSoapEnemy.IsEmpty)
                _roundFinish.Raise();
        }
        
        public void StartRound()
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].gameObject.SetActive(false);
                _characters[i].gameObject.SetActive(true);
            }
            
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].gameObject.SetActive(true);
            }
        }

        public void StartCountDown()
        {
            Timing.RunCoroutine(StartCountDownTime(),Segment.FixedUpdate);
        }

        private IEnumerator<float> StartCountDownTime()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);   
                _timeRound.Value--;
            }
        }

        private IEnumerator<float> RoundCheck()
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].gameObject.SetActive(true);
            }
            
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].gameObject.SetActive(true);
            }
            yield return 0;
        }
    }
}
