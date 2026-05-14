using System.Collections.Generic;
using Game.Runtime.Infastructure;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Services.Implementation;
using UnityEngine;

namespace Game.Runtime
{
    public static class MobSpawner
    {
        private static IGameLog Log => ServiceResolver.Resolve<IGameLog>();
        
        public static void Activate(GameObject player, (List<GameObject> prefabs, int count) enemies, (List<GameObject> prefabs, int count) collectibles)
        {
            PlacePlayer(player);
            PlaceEnemies(enemies.prefabs, enemies.count);
            PlaceCollectables(collectibles.prefabs, collectibles.count);
        }
        
        static void PlacePlayer(GameObject player)
        {
            Place(player);
        }
        
        private static GameObject RandomPrefab(List<GameObject> placeables) => placeables[Random.Range(0, placeables.Count)];

        private static void PlaceEnemies(List<GameObject> placeables, int count)
        {
            for (int i = 0; i < count; i++) Place(RandomPrefab(placeables));
        }

        private static void PlaceCollectables(List<GameObject> placeables, int count)
        {
            for (int i = 0; i < count; i++) Place(RandomPrefab(placeables));
        }

        static void Place(GameObject placeable)
        {
            var moveService = ServiceResolver.Resolve<MoveService>();
            
            int randomRow = Random.Range(0, moveService.GridDimensions.numRows);
            int randomCol = Random.Range(0, moveService.GridDimensions.numColumns);
    
            GameObject spawnedPlaceable = Object.Instantiate(placeable, new Vector3(randomCol, randomRow, -1), Quaternion.identity);
            
            Log.Info($"Placing {placeable.name} at {randomCol}, {randomRow}");
    
            spawnedPlaceable.transform.position = new Vector3(randomCol, randomRow, 0);
        }
    }
}
