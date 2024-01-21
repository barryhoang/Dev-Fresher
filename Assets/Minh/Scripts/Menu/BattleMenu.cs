using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minh
{
    public class BattleMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private GameObject _tileMap;
        [SerializeField] private GameObject _placementManager;
        [SerializeField] private GameObject _fightingButton;
        public void ReturnToMenuScene()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void NextLevel()
        {
            _nextButton.SetActive(false);
            _tileMap.SetActive(true);
            _placementManager.SetActive(true);
            _fightingButton.SetActive(true);
        }
    }
}