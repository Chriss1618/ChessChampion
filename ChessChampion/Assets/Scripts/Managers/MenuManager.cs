using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private GameObject _selectedHeroObject, _tileMenuObject, _TileUnitObject;

    private void Awake() {
        instance = this;
    }

    public void ShowTileInfo(Tile tile) {
        if (tile == null) {
            _tileMenuObject.SetActive(false);
            _TileUnitObject.SetActive(false);
            return;
        }

        
        _tileMenuObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileMenuObject.SetActive(true);

        if(tile.OccupiedUnit ){
            _TileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _TileUnitObject.SetActive(true);
        }
    }
    public void ShowSelectedHero(BaseHero hero ) {

        if (hero == null){
            _selectedHeroObject.SetActive( false );
            return;
        }
        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _selectedHeroObject.SetActive( true );
    }
}
