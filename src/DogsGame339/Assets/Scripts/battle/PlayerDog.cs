using Game.Runtime;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace battle
{
    public class PlayerDog : BattleDog
    {
        public override void Attack()
        {
            Debug.Log("Player's dog attacks!");
        }

        public override void Flee()
        {
            Debug.Log("Player's dog turns tail and runs away!");
            SceneManager.LoadScene("walkingScene");
            ServiceResolver.Resolve<GameState>().Player.TakeDamage(1);
            //update health -1
        }

        public void Check()
        {
            Debug.Log(@"Player examines the opponent...
Name:   [MISSINGNO]
Size:     UNKNOWN
Health:     0/0
Attack:      ?
Speed:       ?");
        }
    }
}
