using System;
using UnityEngine;

namespace Tung
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _listSoapEnemy;


        private void Update()
        {
            CheckEnemy();
        }

        private void CheckEnemy()
        {
           
        }

        private void OnDestroy()
        {
            _listSoapEnemy.OnCleared -= CheckEnemy;
        }
    }
}
