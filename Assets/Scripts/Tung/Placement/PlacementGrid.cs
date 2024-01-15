using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 _eventDrag;
        [SerializeField] private ScriptableEventVector2 _eventDown;
        [SerializeField] private ScriptableEventVector2 _eventUp;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private GameObject _gridMouse;
        [SerializeField] private SpriteRenderer _spriteMouse;
        private Unit character;
        private bool isDragg;
        private Vector2Int posBefore;
        
        private void OnEnable()
        {
            _eventDown.OnRaised += CheckUnitInCell;
            _eventDrag.OnRaised += DragCharacter;
            _eventUp.OnRaised += SetGridUnit;
        }

        private void CheckUnitInCell(Vector2 value)
        {
            Vector2Int mousePos = value.ToV2Int();
            if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x >= 6 || mousePos.y >= 6) return;
            if (_gridMap.Value[mousePos.x, mousePos.y] == null) return;
            
            character = _gridMap.Value[mousePos.x, mousePos.y]; 
            _spriteMouse.sprite = character.unitRenderData.avatar;
            _gridMap.Value[mousePos.x, mousePos.y] = null; 
            posBefore = mousePos;
            isDragg = true;
        }
        
        private void DragCharacter(Vector2 value)
        {
            if(!isDragg) return;
            Vector2Int mousePos = value.ToV2Int();
            if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x >= 6 || mousePos.y >= 6)
            {
                SetMouseRender(false);
            }
            else
            {
                SetMouseRender(true);
                if (_gridMap.Value[mousePos.x, mousePos.y] != null)
                {
                    _spriteMouse.gameObject.SetActive(false);
                }
                else
                {
                    _spriteMouse.gameObject.SetActive(true);
                    _spriteMouse.transform.position = new Vector3(mousePos.x,mousePos.y);
                }
                _gridMouse.transform.position = new Vector3(mousePos.x,mousePos.y);
            }
            character.transform.position = value;
        }
        
        private void SetGridUnit(Vector2 value)
        {
            if (!isDragg) return;
            
            SetMouseRender(false);
             isDragg = false;
             Vector2Int mousePos = value.ToV2Int();
            if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x >= 6 || mousePos.y >= 6)
            {
                SetGridFull(posBefore);
            }
            else if(_gridMap.Value[mousePos.x,mousePos.y] == null)
            {
                SetGridFull(mousePos);
            }
            else
            {
                Unit temp = _gridMap.Value[mousePos.x,mousePos.y];
                SetGridFull(mousePos);
                temp.transform.position = new Vector3(posBefore.x,posBefore.y);
                _gridMap.Value[posBefore.x, posBefore.y] = temp;
            }
        }

        private void SetMouseRender(bool active)
        {
            _spriteMouse.gameObject.SetActive(active);
            _gridMouse.SetActive(active);
        }
        private void SetGridFull(Vector2Int mousePos)
        {
            character.transform.position = new Vector3(mousePos.x,mousePos.y);
            _gridMap.Value[mousePos.x,mousePos.y] = character;
        }
        
        private void OnDisable()
        {
            _eventDown.OnRaised -= CheckUnitInCell;
            _eventDrag.OnRaised -= DragCharacter;
            _eventUp.OnRaised -= SetGridUnit;
        }
    }
}