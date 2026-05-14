using Game.Runtime.Infastructure;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using UnityEngine;

namespace Game.Runtime.World
{
    public class WorldTile : MonoBehaviour
    {
        [SerializeField] private Sprite baseColor, offsetColor;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject highlight;
        private GridPosition _position;
        
        private static IGameLog _log;
        private static I_MoveService _moveService;
        private static GameModel _gameModel;
        
        private void Awake()
        {
            _log ??= ServiceResolver.Resolve<IGameLog>();
            _moveService ??= ServiceResolver.Resolve<I_MoveService>();
            _gameModel ??= ServiceResolver.Resolve<GameModel>();
            
            WorldGenerator.BuildTile += Init;
        }

        private void Init(GameObject tile, GridPosition position, bool isOffset)
        {
            if (tile != gameObject) return;
            
            spriteRenderer.sprite = isOffset ? offsetColor : baseColor;
            _position = position;
            
            WorldGenerator.BuildTile -= Init;
        }

        void OnMouseEnter()
        {
            if (!_gameModel.IsPlaying.Value) return;
            
            highlight.SetActive(true);
        }

        private void OnMouseDown()
        {
            if (!highlight.activeSelf) return;
            
            _log.Info($"Clicked {name}");
        }

        void OnMouseExit()
        {
            if (highlight.activeSelf) highlight.SetActive(false);
        }
    }
}
