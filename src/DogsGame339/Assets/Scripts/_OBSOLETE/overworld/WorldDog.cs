using UnityEngine;

namespace overworld
{
    public abstract class WorldDog : DogObject
    {
        protected static GameObject CreateDog(DogSize size)
        {
            string prefabPath = "prefabs/dogs/";
        
            switch (size)
            {
                case DogSize.Small:
                    prefabPath += "smallDog";
                    break;
            
                case DogSize.Medium:
                    prefabPath += "mediumDog";
                    break;
            
                case DogSize.Large:
                    prefabPath += "bigDog";
                    break;
            }
        
            return Instantiate(Resources.Load<GameObject>(prefabPath));
        }
    }
}