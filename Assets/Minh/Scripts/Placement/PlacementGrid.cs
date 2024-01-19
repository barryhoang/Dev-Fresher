using System.Collections.Generic;
using Obvious.Soap;
using MEC;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Minh
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private Tilemap _playerPlacementGrid;
        [SerializeField] private ScriptableListPlayer _soapListHero;
        [SerializeField] private ScriptableEventVector2 _onButtonDown;
        [SerializeField] private ScriptableEventVector2 _onbuttonUp;
        [SerializeField] private FightingMapVariable _fightingMap;
        [SerializeField] private MouseDrag _mouseDrag;
        [SerializeField] private GameObject _placementGrid;
        private Camera _camera;
        private bool isDragging;
        private Hero savedHero;
        private Vector3 heroPosition;

        // Start is called before the first frame update
        void Start()
        {
            _onButtonDown.OnRaised += CheckHeroPosition;
            //  _onbuttonDrag.OnRaised += MovePlayerPosition;
            _onbuttonUp.OnRaised += PlaceHero;
        }

        // De convert mousePosition sang Int de check grid xem co Hero trong grid hay khong 
        // Neu check duoc thi se set bool Dragging = true va luu lai Hero
        private void CheckHeroPosition(Vector2 mousePos)
        {
            Vector2Int MousePosInt = mousePos.ToV2Int();
            if (MousePosInt.x >= 0 && MousePosInt.x < 6 && MousePosInt.y < 6 && MousePosInt.y >= 0)
            {
                Debug.Log(MousePosInt + "MOUSE POSITION ");
                if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
                {
                    isDragging = true;
                    heroPosition = _fightingMap.Value[MousePosInt.x, MousePosInt.y].transform.position;
                    StartCoroutine(MoveHeroPosition(_fightingMap.Value[MousePosInt.x, MousePosInt.y]));
                    _placementGrid.SetActive(true);
                    _placementGrid.transform.position = new Vector3(MousePosInt.x, MousePosInt.y, 0);
                    _fightingMap.Value[MousePosInt.x, MousePosInt.y] = null;
                }
            }
        }

        //De di chuyen con nhan vat Hero theo vi tri INT cua con chuot
        IEnumerator<float> MoveHeroPosition(Hero _hero)
        {
            while (isDragging)
            {
                Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int MousePosInt = mouseInput.ToV2Int();
                _hero.gameObject.transform.position = mouseInput;
                savedHero = _hero;
                if (MousePosInt.x >= 0 && MousePosInt.x < 6 && MousePosInt.y < 6 && MousePosInt.y >= 0)
                {
                    _placementGrid.transform.position = new Vector3(MousePosInt.x, MousePosInt.y, 0);
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        // De dat nguoi choi nhan vat theo vi tri Mouse Position INT luc ma con chuot tha? ra
        // Neu o duoc tha co con nhan vat khac thi 2 con nhan vat se doi vi tri cho nhau
        private void PlaceHero(Vector2 mousePos)
        {
            if (savedHero == null) return;
            isDragging = false;
            Vector2Int MousePosInt = mousePos.ToV2Int();
              
            if (MousePosInt.x < 0 || MousePosInt.x >= 6 || MousePosInt.y >= 6 || MousePosInt.y < 0)
            {
                savedHero.transform.position = heroPosition;
                var heroPositionInt = heroPosition.ToV2Int();
                _fightingMap.Value[heroPositionInt.x, heroPositionInt.y] = savedHero;
            }
            else if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
            {
                _fightingMap.Value[MousePosInt.x, MousePosInt.y].transform.position = heroPosition;
                var heroPositionInt = heroPosition.ToV2Int();
                _fightingMap.Value[heroPositionInt.x, heroPositionInt.y] = _fightingMap.Value[MousePosInt.x, MousePosInt.y];
                MovePlayer(MousePosInt);
                savedHero = null;
            }
            else
            {
                Debug.Log("MouseX" + MousePosInt.x + "MouseY" + MousePosInt.y);
                MovePlayer(MousePosInt);
                savedHero = null;
            }

            _placementGrid.SetActive(false);
        }
        
        private void MovePlayer(Vector2Int MousePosInt)
        {
            savedHero.transform.position = new Vector3(MousePosInt.x, MousePosInt.y, 0);
            _fightingMap.Value[MousePosInt.x, MousePosInt.y] = savedHero;
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            _onButtonDown.OnRaised -= CheckHeroPosition;
            _onbuttonUp.OnRaised -= PlaceHero;
        }
    }
}