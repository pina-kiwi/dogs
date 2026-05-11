using System.Collections.Generic;
using System.Linq;

namespace Game339.Shared.Models
{
    public class GameState
    {
        public readonly Character Player;
        public readonly List<Character> Enemies;

        public ObservableValue<bool> IsPlaying { get; }
        public ObservableValue<int> PlayerBones { get; }
        public ObservableValue<int> CurrentTurn { get; }

        public GameState(Character player, IEnumerable<Character> enemies, int playerStartingBones)
        {
            Player = player;
            Enemies = enemies.ToList();

            IsPlaying = new();
            PlayerBones = new(playerStartingBones);
            CurrentTurn = new();
        }

        public void SetBones(int bones) => PlayerBones.Value = bones;

        public void AdvanceTurn() => CurrentTurn.Value++;
    }
}