using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Component.EventSystem;
using Component.PoolSystem;
using Component.SettingSystem;
using Component.ThemeSystem;
using Domain.Constants;
using Domain.EventClasses;
using Presentation.Game.Card;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _gameRoot;
        [SerializeField] private Transform _cardStartPlaceTransform;
        [SerializeField] private GameObject _eachCardPrefab;
        [SerializeField] private Transform _gameGoldenFrame;
        [SerializeField] private GameLogic _gameLogic;

        private int _width;
        private int _height;

        private readonly List<GameObject> _cardObjects = new List<GameObject>();


        private void Awake()
        {
            PoolService.AddObjectToPool(PoolItemIds.GAME_CARD, _eachCardPrefab);
        }

        private void OnEnable()
        {
            EventService.Subscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
        }

        private async void OnGameStartLoading(OnGameStartLoading onGameStartLoading)
        {
            Reset();

            await Task.Delay(1000);

            if (onGameStartLoading.IsFirstTime)
            {
                _width = onGameStartLoading.Width;
                _height = onGameStartLoading.Height;
            }

            var task = ThemeService.GetCards();
            await task;

            var cards = RandomizeCards(task.Result.Textures.ToList());
            CreateLevel(cards);
            SetLevelScale();

            await Task.Delay(1000);

            EventService.Invoke<bool>(GameEvents.ON_GAME_FINISH_LOADING, true);
            _gameLogic.StartGame(_width, _height, onGameStartLoading.IsFirstTime);
        }

        private void Reset()
        {
            for (int i = 0; i < _cardObjects.Count; i++)
            {
                PoolService.BackToPool(_cardObjects[i]);
            }

            _cardObjects.Clear();
            _gameRoot.transform.localScale = Vector3.one;
        }

        private List<Texture2D> RandomizeCards(List<Texture2D> cards)
        {
            List<Texture2D> temp = new List<Texture2D>();
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
            float offsetX = (_width - 1) * SettingService.CardWidth / 2.0f;
            float offsetY = (_height - 1) * SettingService.CardHeight / 2.0f;

            int cardIndex = 0;

            var themeTexture = ThemeService.GetThemeSprite();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var card = PoolService.GetItem(PoolItemIds.GAME_CARD);
                    card.transform.SetParent(_gameRoot);
                    card.transform.localScale = Vector3.one;
                    _cardObjects.Add(card);

                    float x = j * SettingService.CardHeight - offsetY;
                    float y = i * SettingService.CardWidth - offsetX;
                    card.GetComponent<Card>().Initialize(themeTexture, cards[cardIndex], x, y, OnClickOnCard);
                    cardIndex++;
                }
            }
        }

        private void SetLevelScale()
        {
            float widthScale = _gameGoldenFrame.localScale.x / (_height * SettingService.CardHeight);
            float heightScale = _gameGoldenFrame.localScale.y / (_width * SettingService.CardWidth);

            float scale = Mathf.Min(widthScale, heightScale);
            _gameRoot.transform.localScale = Vector3.one * scale;
        }

        private void OnClickOnCard(Card card)
        {
            _gameLogic.SelectCard(card);
        }
    }
}