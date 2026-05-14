using System.Collections.Generic;
using Game.Runtime.Infastructure;
using Game339.Shared.Infastructure.Diagnostics;
using UnityEngine;

namespace Game.Runtime
{
    public class Game : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField] private int NumberOfRows, NumberOfColumns;
        [SerializeField] private List<GameObject> TilePrefabs;
        
        [Header("Player")]
        [SerializeField] private GameObject PlayerPrefab;
        [SerializeField] private int MaximumBones;
        [SerializeField] private int StartingBones;
    
        [Header("Enemies")]
        [SerializeField] private List<GameObject> EnemyPrefabs;
        [SerializeField] private int NumberOfEnemies;
        
        [Header("Collectables")]
        [SerializeField] private List<GameObject> CollectablePrefabs;
        [SerializeField] private int NumberOfCollectables;
        
        private void Awake()
        {
            IGameLog log = ServiceResolver.Resolve<IGameLog>();

            ServiceResolver.Register<(int numRows, int numColumns)>((NumberOfRows, NumberOfColumns));
            ServiceResolver.Register<(int maxBones, int initialBones)>((MaximumBones, StartingBones));
            
            WorldGenerator.Wake(TilePrefabs);
            if (Camera.main != null)
                Camera.main.transform.position = new Vector3((float)NumberOfColumns / 2 - .5f, (float)NumberOfRows / 2, -10);
            log.Info("Level Generation Complete");

            MobSpawner.Activate(PlayerPrefab,
                (EnemyPrefabs, NumberOfEnemies),
                (CollectablePrefabs, NumberOfCollectables));
            log.Info("Entity Placement Complete");
        }
    }
}
