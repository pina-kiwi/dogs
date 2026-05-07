using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;
using UnityEngine.UI;

namespace overworld
{
    public class PlayerHpView : ObserverMonoBehaviour
    {
        [SerializeField] private Sprite fullHeart, halfHeart, emptyHeart;
        [SerializeField] private GameObject heartPrefab;
        private bool _maxHealthSet;

        private int MaxHealth { get {
            int maxHealth_holder = GameState.Player.MaxHealth.Value;
            if (maxHealth_holder == int.MaxValue) maxHealth_holder = GameState.Player.Health.Value;
            return maxHealth_holder;
        } }

        private static GameState GameState => ServiceResolver.Resolve<GameState>();

        protected override void Subscribe()
        {
            GameState.Player.MaxHealth.ChangeEvent += PlayerMaxHealthChange;
            GameState.Player.Health.ChangeEvent += PlayerHealthChange;
        }

        protected override void Unsubscribe()
        {
            GameState.Player.MaxHealth.ChangeEvent += PlayerMaxHealthChange;
            GameState.Player.Health.ChangeEvent -= PlayerHealthChange;
        }
        
        private void PlayerMaxHealthChange(int changedMaxHealth)
        {
            //print(health);

            if (_maxHealthSet) return;

            GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
            foreach(GameObject heart in hearts) if (heart != null) Destroy(heart);
            
            for (int i = 0; i < MaxHealth; i += 2)
            {
                Instantiate(heartPrefab, gameObject.transform);
            }
            _maxHealthSet = true;
        }

        private void PlayerHealthChange(int changedHealth)
        {
            int health_holder = GameState.Player.Health.Value;
            
            foreach(Transform child in gameObject.transform)
            {
                health_holder -= 2;
                Image image = child.GetComponent<Image>();

                image.sprite = health_holder switch
                {
                    > -1 => fullHeart,
                    -1 => halfHeart,
                    _ => image.sprite
                };
                if (health_holder < -1) break;
            }
        }
    }
}