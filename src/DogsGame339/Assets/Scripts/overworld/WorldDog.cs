using System;
using UnityEngine;

namespace overworld
{
    public abstract class WorldDog : DogObject
    {
        public void SetSize(DogSize newSize) => size = newSize;

        public static GameObject CreateDog(DogSize size)
        {
            string prefabPath = "";
        
            switch (size)
            {
                case DogSize.Small:
                    prefabPath = "Assets/Prefabs/smallDog";
                    break;
            
                case DogSize.Medium:
                    prefabPath = "Assets/Prefabs/mediumDog";
                    break;
            
                case DogSize.Large:
                    prefabPath = "Assets/Prefabs/bigDog";
                    break;
            }
        
            return Instantiate(Resources.Load<GameObject>(prefabPath));
        }

        private void Start()
        {
            if (!gameObject.transform.parent.GetComponent<PlayerCharacter>()) {
                EnemyDog enemyDogComponent = gameObject.AddComponent<EnemyDog>();
                enemyDogComponent.SetSize(size);
            }
            
            Destroy(this);
        }
    }
}