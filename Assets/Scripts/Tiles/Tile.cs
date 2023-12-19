using UnityEngine;


public abstract class Tile : MonoBehaviour
{ 
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public string TileName;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    
    public virtual void Init(int x, int y)
    {
        
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        //MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        //MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.gameState != GameState.HeroesTurn) return;
        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Heroes) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (UnitManager.Instance.selectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.selectedHero != null){
                SetUnit(UnitManager.Instance.selectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
        
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
