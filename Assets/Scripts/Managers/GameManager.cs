using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Component.EventSystem;
using Component.PoolSystem;
using Component.ThemeSystem;
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
        [SerializeField] private Transform _gameGoldenFrame;

        private int _width;
        private int _height;
        private List<Texture2D> _cards;
        private Texture2D _theme;

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

        private async void OnGameStart(OnGameStart onGameStart)
        {
            await Task.Delay(1000);

            _width = onGameStart.Width;
            _height = onGameStart.Height;

            var task = ThemeService.GetCards();
            await task;

            _cards = task.Result.Textures.ToList();
            _theme = ThemeService.GetThemeSprite();

            var cards = RandomizeCards();
            CreateLevel(cards);
            SetLevelScale();
        }

        private List<Texture2D> RandomizeCards()
        {
            List<Texture2D> temp = new List<Texture2D>();
            List<Texture2D> cards = _cards.ToList();

            for (int i = 0; i < _width * _height / 2; i++)
            {
                var random = Random.Range(0, cards.Count);
                temp.Add(cards[random]);
                temp.Add(cards[random]);
                cards.RemoveAt(random);
            }

            cards.Clear();

            int count = temp.Count;
            for (int i = 0; i < count; i++)
            {
                var random = Random.Range(0, temp.Count);
                cards.Add(temp[random]);
                temp.RemoveAt(random);
            }

            return cards;
        }

        private void CreateLevel(List<Texture2D> cards)
        {
            float offsetX = (_width - 1) * CARD_WIDTH / 2.0f;
            float offsetY = (_height - 1) * CARD_HEIGHT / 2.0f;

            int cardIndex = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var card = PoolService.GetItem(PoolItemIds.GAME_CARD);
                    card.transform.SetParent(_gameRoot);
                    card.transform.position = _cardStartPlaceTransform.position;

                    card.GetComponent<Card>().Initialize(_theme, cards[cardIndex], j * CARD_HEIGHT - offsetY, i * CARD_WIDTH - offsetX);
                    cardIndex++;
                }
            }
        }

        private void SetLevelScale()
        {
            float widthScale = _gameGoldenFrame.localScale.x / (_height * CARD_HEIGHT);
            float heightScale = _gameGoldenFrame.localScale.y / (_width * CARD_WIDTH);

            float scale = Mathf.Min(widthScale, heightScale);
            _gameRoot.transform.localScale = Vector3.one * scale;
        }
    }
}