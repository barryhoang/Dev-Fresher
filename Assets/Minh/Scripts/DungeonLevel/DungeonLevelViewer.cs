using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace Minh
{
    public class DungeonLevelViewer : UnityEngine.MonoBehaviour
    {
        [SerializeField]private IntVariable _currentLevel;
        [SerializeField]private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _enemyLeft;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;

        private void Update()
        {
            _currentLevelText.text = "LEVEL: "+ _currentLevel.Value.ToString();
            _enemyLeft.text = _soapListEnemy.Count + "/" + _currentLevel.Value;
        }
    }
}