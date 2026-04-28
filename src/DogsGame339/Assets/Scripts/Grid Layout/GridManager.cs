using Game339.Shared.Diagnostics;
using Game339.Shared.Services.Implementation;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tile;
    public GameObject collectable;
    public GameObject smallDog;
    public GameObject player;
    
    public SpriteRenderer spriteRenderer;

    private readonly IGameLog _gameLog;

    public Transform cam;

    public GridManager(IGameLog gameLog)
    {
        _gameLog = gameLog;
    }

    void Start()
    {
        GenerateGrid();
        PlaceDogs();
        PlaceCollectables();
        PlacePlayer();
    }
    
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = "Tile: " + x + ", " + y;
                
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        cam.transform.position = new Vector3((float)width/2-.5f, (float)height/2, -10);
    }

    void PlacePlayer()
    {
        Place(player);
    }
    
    private void PlaceDogs()
    {
        //randomize dog placement
        Place(smallDog);
    }

    private void PlaceCollectables()
    {
        //randomize bone placement
        Place(collectable);
    }

    Vector2 getRandomTile()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        
        return new Vector2(x, y);
    }

    void Place(GameObject placeable)
    {
        Vector3 randomTile = getRandomTile();
        //_gameLog.Info($"Placing {placeable.name} at {randomTile}");
        //Debug.Log(randomTile);
        var spawnedPlaceable = Instantiate(placeable, new Vector3(randomTile.x, randomTile.y, 10), Quaternion.identity);
        //spawnedPlaceable.name = name + ": " + randomTile.x + ", " + randomTile.y + ", " + randomTile.z;
        
        spawnedPlaceable.transform.position = randomTile;
    }
}
