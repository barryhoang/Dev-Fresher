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
        [SerializeField] private ScriptableListHero _soapListHero;
        [SerializeField] private ScriptableEventVector2 _onButtonDown;
        [SerializeField] private ScriptableEventVector2 _onbuttonUp;
        [SerializeField] private FightingMapVariable _fightingMap;
        [SerializeField] private MouseDrag _mouseDrag;
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
            Vector2Int MousePosInt = new Vector2Int((int) Mathf.Round(mousePos.x), (int) Mathf.Round(mousePos.y));
            
            if (MousePosInt.x >= 0 && MousePosInt.x < 6 && MousePosInt.y < 6 && MousePosInt.y >= 0)
            {
                Debug.Log(MousePosInt + "MOUSE POSITION ");
                if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
                {
                    isDragging = true;
                    heroPosition = _fightingMap.Value[MousePosInt.x, MousePosInt.y].transform.position;
                    StartCoroutine(MoveHeroPosition(_fightingMap.Value[MousePosInt.x, MousePosInt.y]));
                    _fightingMap.Value[MousePosInt.x, MousePosInt.y] = null;
                }
            }
            else
            {
                return;
            }
        }

        //De di chuyen con nhan vat Hero theo vi tri INT cua con chuot
        IEnumerator<float> MoveHeroPosition(Hero _hero)
        {
            while (isDragging)
            {
                Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _hero.gameObject.transform.position = mouseInput;
                savedHero = _hero;
                yield return Timing.WaitForOneFrame;
            }
        }

        // De dat nguoi choi nhan vat theo vi tri Mouse Position INT luc ma con chuot tha? ra
        // Neu o duoc tha co con nhan vat khac thi 2 con nhan vat se doi vi tri cho nhau
        private void PlaceHero(Vector2 mousePos)
        {
            isDragging = false;
            if (savedHero != null)
            {
                Vector2Int MousePosInt =
                    new Vector2Int((int) Mathf.Round(mousePos.x), (int) Mathf.Round(mousePos.y));
                if (MousePosInt.x >= 0 && MousePosInt.x < 6 && MousePosInt.y < 6 && MousePosInt.y >= 0)
                {
                    if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
                    {
                        _fightingMap.Value[MousePosInt.x, MousePosInt.y].transform.position = heroPosition;
                        savedHero.transform.position = new Vector3(MousePosInt.x, MousePosInt.y, 0);
                        _fightingMap.Value[Mathf.RoundToInt(heroPosition.x), Mathf.RoundToInt(heroPosition.y)] =
                            _fightingMap.Value[MousePosInt.x, MousePosInt.y];
                        _fightingMap.Value[MousePosInt.x, MousePosInt.y] = savedHero;
                        savedHero = null;
                    }
                    else
                    {
                        Debug.Log("MouseX"+MousePosInt.x+"MouseY"+MousePosInt.y);
                        savedHero.transform.position = new Vector3(MousePosInt.x, MousePosInt.y, 0);
                        _fightingMap.Value[MousePosInt.x, MousePosInt.y] = savedHero;
                        savedHero = null;
                    }
                }
            }
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            _onButtonDown.OnRaised -= CheckHeroPosition;
            _onbuttonUp.OnRaised -= PlaceHero;
        }
    }
}