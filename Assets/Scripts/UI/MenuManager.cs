using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _selectedHeroObject;
    public GameObject _tileObject;
    public GameObject _tileUnitObject;
    public static MenuManager Instance;
    
    private void Awake() => Instance = this;

    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }
        _tileObject.GetComponent<Text>().text = tile.TileName;
        _tileObject.SetActive(true);
        if (tile.OccupiedUnit)
        {
            _tileUnitObject.GetComponent<Text>().text = tile.OccupiedUnit.unitName;
            _tileUnitObject.SetActive(true);
        }
    }


    public void ShowSelectedHero(BaseHero hero)
    {
        if (hero == null)
        {
            _selectedHeroObject.SetActive(false);
            return;
        }
        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.unitName;
        _selectedHeroObject.SetActive(true);
    }
}
