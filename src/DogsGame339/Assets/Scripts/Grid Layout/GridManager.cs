using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tile;

    public Transform cam;

    void Start()
    {
        GenerateGrid();
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
        cam.transform.position = new Vector3((float)width/2-.5f, (float)height/2-.5f, -10);
    }
}
