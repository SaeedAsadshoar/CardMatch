using Component.EventSystem;
using Component.PoolSystem;
using Domain.Constants;
using Domain.EventClasses;
using Presentation.Game;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const float CARD_WIDTH = 1.5f;
        private const float CARD_HEIGHT = 1.5f;

        [SerializeField] private Transform _gameRoot;
        [SerializeField] private Transform _cardStartPlaceTransform;
        [SerializeField] private GameObject _eachCardPrefab;

        private int _width;
        private int _height;

        private void Awake()
        {
            PoolService.AddObjectToPool(PoolItemIds.GAME_CARD, _eachCardPrefab);
        }

        private void OnEnable()
        {
            EventService.Subscribe<OnGameStart>(GameEvents.ON_GAME_START, OnGameStart);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<OnGameStart>(GameEvents.ON_GAME_START, OnGameStart);
        }

        private void OnGameStart(OnGameStart onGameStart)
        {
            _width = onGameStart.Width;
            _height = onGameStart.Height;

            float offsetX = (_width - 1) * CARD_WIDTH / 2.0f;
            float offsetY = (_height - 1) * CARD_HEIGHT / 2.0f;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var card = PoolService.GetItem(PoolItemIds.GAME_CARD);
                    card.transform.SetParent(_gameRoot);
                    card.transform.position = _cardStartPlaceTransform.position;

                    card.GetComponent<Card>().Inititialize(j * CARD_HEIGHT - offsetY, i * CARD_WIDTH - offsetX);
                }
            }
        }
    }
}