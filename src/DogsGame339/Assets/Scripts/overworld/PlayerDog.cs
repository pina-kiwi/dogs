using UnityEngine;

namespace overworld
{
    public class PlayerDog : WorldDog
    {
        public new static GameObject CreateDog(DogSize size)
        {
            GameObject dogObject = WorldDog.CreateDog(size);
            dogObject.AddComponent<PlayerDog>();
            Destroy(dogObject.GetComponent<EnemyDog>());
            dogObject.transform.Translate(-2.5f, 0, 0, Space.World);
            
            return dogObject;
        } 
    }
}