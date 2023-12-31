using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private Transform _Board;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tileGrass,_tileMountain;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    private void Awake() {
        Instance = this;
    }
    
    public void GenerateGrid() { 
        _tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < _width; x++) {
            for (int y = 0; y< _height; y++) {
                var randomTile = Random.Range(0, 6) == 3 ? _tileMountain : _tileGrass;
                var spawnedTile = Instantiate(randomTile, new Vector3(x,y),Quaternion.identity);
                spawnedTile.GetComponent<Transform>().parent = _Board;
                spawnedTile.name = $"Tile {x}{y}";
                spawnedTile.Init(x,y);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height / 2 - 0.5f, - 10);

        GameManager.Instance.ChangeState(GameState.SpawnHeros);
    }

    public Tile GetHeroSpawnTile() { 
        return _tiles.Where(t=> t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;

    }

    public Tile GetEnemySpawnTile() {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;

    }
}
