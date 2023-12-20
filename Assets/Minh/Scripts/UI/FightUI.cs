using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class FightUI : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _onFight;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        public void ReadyToFight()
        {
            _onFight.Raise();
        }
    }
}