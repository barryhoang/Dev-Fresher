using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam onLose;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private GameObject defeatedPanel;
        [SerializeField] private GameObject victoryPanel;
        
        public void OnMainMenuClickButton () => SceneManager.LoadScene(sceneBuildIndex: 0);
        private void Start()
        {
            onLose.OnRaised += SetDefeatedPanel;
            onVictory.OnRaised += SetVictoryPanel;
        }

        private void SetDefeatedPanel()
        {
            defeatedPanel.SetActive(true);
        }
    
        private void SetVictoryPanel()
        {
            victoryPanel.SetActive(true);
        }
    }
}
