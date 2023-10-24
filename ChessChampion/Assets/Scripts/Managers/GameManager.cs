using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState _GameState;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    void Update()
    {
        
    }


    public void ChangeState(GameState gameState) {
        this._GameState = gameState;
        switch (_GameState) {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeros:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                break;
            case GameState.EnemiesTurn:
                break;
        }
    }
   
}

public enum GameState {
    GenerateGrid = 0,
    SpawnHeros = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}
