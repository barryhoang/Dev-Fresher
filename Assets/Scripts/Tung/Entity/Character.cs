using UnityEngine;

namespace Tung
{
    public class Character : Entity
    {

        [SerializeField] private GameObject _spritesFlowCharacter;
        [SerializeField] private ScriptableListCharacter _listSoapCharacter;

        public Vector2Int sizePos;

        private void OnMouseDown()
        {
            _gridMap.Value[(int)transform.position.x, (int)transform.position.y] = false;
            _spritesFlowCharacter.SetActive(true);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            _spritesFlowCharacter.transform.position = cellPosition;
        }

        private void OnMouseDrag()
        {
            if (transform.position.x >= sizePos.x || transform.position.x <= 1 || transform.position.y >= sizePos.y ||
                transform.position.y <= 1)
            {
                _spritesFlowCharacter.SetActive(false);
            }
            else
            {
                _spritesFlowCharacter.SetActive(true);
            }
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            _spritesFlowCharacter.transform.position = cellPosition;

        }

        private void OnMouseUp()
        {
            _spritesFlowCharacter.SetActive(false);
            Vector3Int cellPosition = _tilemap.WorldToCell(transform.position);
            transform.position = cellPosition;
            if (transform.position.x > sizePos.x || transform.position.x < 1 || transform.position.y > sizePos.y ||
                transform.position.y < 1)
            {
                transform.position = posStart;
            }
            foreach (var entity in _listSoapCharacter)
            {
                if (entity != this && entity.transform.position == transform.position)
                {
                    entity.transform.position = posStart;
                    entity.posStart = posStart;
                    _gridMap.Value[(int)posStart.x, (int)posStart.y] = true;
                }
            }
            _gridMap.Value[(int)transform.position.x, (int)transform.position.y] = true;
            posStart = transform.position;
        }
    }
}
