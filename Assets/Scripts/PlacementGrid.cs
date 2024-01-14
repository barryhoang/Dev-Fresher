using System.Collections.Generic;
using MEC;
using UnityEngine;
using Obvious.Soap;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 onBtnDown;
    [SerializeField] private ScriptableEventVector2 onBtnUp;
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private GameObject placementGrid;
    private bool _dragging;
    private Hero _tempHero;
    private Vector2 _heroPos;


    private void Start()
    {
        onBtnDown.OnRaised += CheckHeroPos;
        onBtnUp.OnRaised += PlaceHero;
    }

    private void CheckHeroPos(Vector2 mousePos)
    {
        var mousePosInt = new Vector2Int(Mathf.RoundToInt(mousePos.x),
            Mathf.RoundToInt(mousePos.y));
        if (mousePosInt.x is < 0 or >= 6 || mousePosInt.y < 0) return;
        _dragging = true;
        var herPos = mapVariable.Value[mousePosInt.x, mousePosInt.y].transform.position;
        StartCoroutine(MoveHero(mapVariable.Value[mousePosInt.x, mousePosInt.y]));
        placementGrid.SetActive(true);
        placementGrid.transform.position = new Vector3(mousePosInt.x, mousePosInt.y, 0);
        mapVariable.Value[mousePosInt.x, mousePosInt.y] = null;
    }
    private IEnumerator<float> MoveHero(Hero hero)
    {
        while (_dragging)
        {
            Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePosInt = new Vector2Int(Mathf.RoundToInt(mouseInput.x),
                Mathf.RoundToInt(mouseInput.y));
            hero.gameObject.transform.position = mouseInput;
            _tempHero = hero;
            if (mousePosInt.x is >= 0 and < 6 && mousePosInt.y is < 6 and >= 0)
            {
                placementGrid.transform.position = new Vector3(mousePosInt.x, mousePosInt.y, 0);
            }

            yield return Timing.WaitForOneFrame;
        }
    }
    
    private void PlaceHero(Vector2 mousePos)
    {
        if (_tempHero == null) return;
        _dragging = false;
        var mousePosInt = new Vector2Int(Mathf.RoundToInt(mousePos.x),
            Mathf.RoundToInt(mousePos.y));

        if (mousePosInt.x < 0 || mousePosInt.x >= 6 || mousePosInt.y >= 6 || mousePosInt.y < 0)
        {
            _tempHero.transform.position = _heroPos;
            var heroPosInt = new Vector2Int(Mathf.RoundToInt(_heroPos.x),
                Mathf.RoundToInt(_heroPos.y));
            mapVariable.Value[heroPosInt.x, heroPosInt.y] = _tempHero;
        }
        else if (mapVariable.Value[mousePosInt.x, mousePosInt.y] != null)
        {
            mapVariable.Value[mousePosInt.x, mousePosInt.y].transform.position = _heroPos;
            var heroPosInt = new Vector2Int(Mathf.RoundToInt(_heroPos.x),Mathf.RoundToInt(_heroPos.y));
            mapVariable.Value[heroPosInt.x, heroPosInt.y] = mapVariable.Value[mousePosInt.x, mousePosInt.y];
            _tempHero.transform.position = new Vector3(mousePosInt.x, mousePosInt.y, 0);
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _tempHero;
            _tempHero = null;
        }
        else
        {
            _tempHero.transform.position = new Vector3(mousePosInt.x, mousePosInt.y, 0);
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _tempHero;
            _tempHero = null;
        }

        placementGrid.SetActive(false);
    }
    
    private void OnDestroy()
    {
        onBtnDown.OnRaised -= CheckHeroPos;
        onBtnUp.OnRaised -= PlaceHero;
    }
}
