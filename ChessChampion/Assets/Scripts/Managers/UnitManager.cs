using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField] private int HeroCount = 5;
    private List<ScriptableUnit> _units;
    
    public BaseHero SelectedHero;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }
    public void SpawnHeroes() {
        for (int i = 0; i < HeroCount; i++) {
            var randomPrefab    = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero     = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            randomSpawnTile.setUnit(spawnedHero);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies() {
        var EnemyCount = 1;
        for (int i = 0; i < EnemyCount; i++) {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.setUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T GetRandomUnit<T>( Faction faction ) where T : BaseUnit{
        //return (T)_units[0].UnitPrefab;
        return (T) _units.Where( u => u.Faction == faction ).OrderBy( o =>Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero) {
        SelectedHero = hero;
        MenuManager.instance.ShowSelectedHero(SelectedHero);
    }
}
