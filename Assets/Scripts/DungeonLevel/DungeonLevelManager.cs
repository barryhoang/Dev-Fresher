using Obvious.Soap;
using Placement;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonLevel
{
    public class DungeonLevelManager : MonoBehaviour
    {
        [SerializeField] private IntVariable currentLv;
        [SerializeField] private ScriptableEventInt spawnEnemies;
        [SerializeField] private Button nextLvButton;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private ScriptableEventNoParam onDefeated;
        [SerializeField] private GameObject lvViewer;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private PlacementManager placementManager;

        private void OnEnable()
        {
            nextLvButton.onClick.AddListener(NextLevel);
            spawnEnemies.Raise(currentLv);
            onFight.OnRaised += ActiveLvViewerPanel;
            onVictory.OnRaised += DeactivateLvViewerPanel;
            onDefeated.OnRaised += DeactivateLvViewerPanel;
        }

        private void ActiveLvViewerPanel() => lvViewer.SetActive(true);

        private void DeactivateLvViewerPanel() => lvViewer.SetActive(false);
        
        private void NextLevel()
        {
            placementManager.gameObject.SetActive(true);
            currentLv.Value++;
            victoryPanel.SetActive(false);
            spawnEnemies.Raise(currentLv);
        }

        private void OnDisable()
        {
            onFight.OnRaised -= ActiveLvViewerPanel;
            onVictory.OnRaised -= DeactivateLvViewerPanel;
            onDefeated.OnRaised -= DeactivateLvViewerPanel;
        }
    }   
}
