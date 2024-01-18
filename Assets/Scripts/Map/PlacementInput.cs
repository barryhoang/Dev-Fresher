using Manager;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 btnDown;
        [SerializeField] private ScriptableEventVector2 btnDrag;
        [SerializeField] private ScriptableEventVector2 btnUp;
        [SerializeField] private Button fightButton;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private GameObject placementGrid;
        [SerializeField] private GameObject fightingManager;

        private void Awake()
        {
            fightButton.gameObject.SetActive(true);
            var button = fightButton.GetComponent<Button>();
            button.onClick.AddListener(StartOnClick);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                btnDown.Raise(GetMousePoint());
            }else if (Input.GetMouseButton(0))
            {
                btnDrag.Raise(GetMousePoint());
            }else if (Input.GetMouseButtonUp(0))
            {
                btnUp.Raise(GetMousePoint());
            }
        }
    
        private static Vector2 GetMousePoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    
        private void StartOnClick()
        {
            gameManager.currentState = GameManager.State.Fight;
            placementGrid.SetActive(false);
            onFight.Raise();
            fightingManager.SetActive(true);
        }
    }
}
