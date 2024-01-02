using UnityEngine;

namespace Entity
{
    public class Character : Entity
    {
        [SerializeField] private GameObject _spritesFlowCharacter;
        private void OnMouseDown()
        {
            _isDrag = true;
            _spritesFlowCharacter.SetActive(true);
            _panelStats.gameObject.SetActive(false);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x,worldPoint.y,0);
            
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            _spritesFlowCharacter.transform.position = cellPosition;
        }

        private void OnMouseDrag()
        {
            _spritesFlowCharacter.SetActive(true);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x,worldPoint.y,0);
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            _spritesFlowCharacter.transform.position = cellPosition;
        }

        private void OnMouseUp()
        {
            _isDrag = false;
            _spritesFlowCharacter.SetActive(false);
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            transform.position = cellPosition;
        }
    }
}
