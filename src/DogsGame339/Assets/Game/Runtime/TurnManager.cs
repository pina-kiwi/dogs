using Game.Runtime.Infastructure;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;

namespace Game.Runtime
{
    public class TurnManager : ObserverMonoBehaviour
    {
        private static IGameLog _log;
        private static GameModel _gameModel;

        private int NumActors => _gameModel.Enemies.Count + 1;
        private int TurnIndex => _gameModel.CurrentTurn.Value % NumActors;
        
        public bool IsPlayersTurn => TurnIndex == 0;
        public GridEntity CharacterOnTurn => IsPlayersTurn ? _gameModel.Player : _gameModel.Enemies[TurnIndex-1];

        protected override void Awake() {
            base.Awake();
            _log = ServiceResolver.Resolve<IGameLog>();
            _gameModel = ServiceResolver.Resolve<GameModel>();
        }
        
        protected override void Subscribe()
        {
            throw new System.NotImplementedException();
        }

        protected override void Unsubscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}