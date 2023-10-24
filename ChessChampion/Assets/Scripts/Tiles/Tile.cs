using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;

    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init( int x, int y ) {
        
    }

    private void OnMouseEnter() {
        _highlight.SetActive(true);
        MenuManager.instance.ShowTileInfo(this);
    }

    private void OnMouseExit() {
        _highlight.SetActive(false);
        MenuManager.instance.ShowTileInfo(null);
    }

    private void OnMouseDown() {
        if (GameManager.Instance._GameState != GameState.HeroesTurn) return;

        if (OccupiedUnit != null) {
            if (OccupiedUnit.Faction == Faction.Hero) {
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            } else {
                if (UnitManager.Instance.SelectedHero != null) {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);

                }
            }
        } else {
            if (UnitManager.Instance.SelectedHero != null) { 
                setUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
        
    }
    public void setUnit(BaseUnit baseUnit) {
        if ( baseUnit.OccupiedTile != null ) baseUnit.OccupiedTile = null;

        if (Walkable) {
            baseUnit.transform.position = transform.position;
            OccupiedUnit = baseUnit;
            baseUnit.OccupiedTile = this;
        }
        

    }

}
