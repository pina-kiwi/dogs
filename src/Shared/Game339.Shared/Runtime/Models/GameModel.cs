using System.Collections.Generic;
using System.Linq;
using Game339.Shared.Infastructure.DataTypes;

namespace Game339.Shared.Models
{
    public class GameModel
    {
        public PlayerDog Player { get; private init; }
        public List<EnemyDog> Enemies { get; private init; }
        public List<Collectables> Collectables { get; private init; }
        
        public ObservableValue<bool> IsPlaying { get; } = new();
        public ObservableValue<int> CurrentTurn { get; } = new();
        
        public GameModel(PlayerDog player, IEnumerable<EnemyDog> enemies, IEnumerable<Collectables> collectables = null)
        {
            Player = player;
            Enemies = enemies.ToList();
            Collectables = collectables?.ToList();
        }

        public void SetIsPlaying(bool isPlaying) => IsPlaying.Value = isPlaying;
        public void AdvanceTurn() => CurrentTurn.Value++;
    }
}