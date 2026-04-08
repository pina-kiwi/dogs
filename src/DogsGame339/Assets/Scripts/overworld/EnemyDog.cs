using battle;
using UnityEngine;

namespace overworld
{
    public class EnemyDog : WorldDog
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject otherObject = collision.gameObject;
            PlayerCharacter player = otherObject.GetComponent<PlayerCharacter>();
            
            if (!player) return;
            
            BattleStage.StartBattle(Card);
        }

        public new static GameObject CreateDog(DogSize size)
        {
            GameObject dogObject = WorldDog.CreateDog(size);
            EnemyDog enemyDogComponent = dogObject.AddComponent<EnemyDog>();
            enemyDogComponent.SetSize(size);
            
            return dogObject;
        } 
    }
}