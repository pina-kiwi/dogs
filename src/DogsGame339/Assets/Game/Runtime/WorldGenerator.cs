using System;
using System.Collections.Generic;
using Game.Runtime.Infastructure;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Runtime
{
    public static class WorldGenerator
    {
        private static IGameLog _log;
        private static List<GameObject> _tilePrefabs;
        
        public static event Action<GameObject, GridPosition, bool> BuildTile;

        public static void Wake(List<GameObject> tilePrefabs)
        {
            _tilePrefabs = tilePrefabs;
            _log = ServiceResolver.Resolve<IGameLog>();
            GenerateGrid();
        }
        
        private static GameObject RandomTile => _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];

        static void GenerateGrid()
        {
            (int gridRows, int gridCols) = ServiceResolver.Resolve<I_MoveService>().GridDimensions;
            
            _log.Info("Generating Terrain...");
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    GameObject spawnedTile = Object.Instantiate(RandomTile, new Vector3(row, col, 0), Quaternion.identity);
                    spawnedTile.name = "Tile: " + row + ", " + col;
            
                    bool isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);
                    BuildTile?.Invoke(spawnedTile, (row, col), isOffset);
                }
            }
        }
    }
}
