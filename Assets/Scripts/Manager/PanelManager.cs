using Obvious.Soap;
using UnityEngine;

namespace Manager
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam onLose;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private GameObject defeatedPanel;
        [SerializeField] private GameObject victoryPanel;
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
