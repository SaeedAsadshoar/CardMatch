using System.Collections.Generic;
using Component.EventSystem;
using Component.StorageSystem;
using Domain.Constants;
using Domain.EventClasses;
using Domain.Structures;
using Presentation.Game.Card;
using UnityEngine;

namespace Managers
{
    public class GameLogic : MonoBehaviour
    {
        private const float EACH_CARD_REST_TIME = 1f;
        private const float EACH_CARD_GAME_TIME = 5f;

        [SerializeField] private List<SelectedCards> _selectedCards = new List<SelectedCards>();

        private float _restTime;
        private float _playTime;
        private float _startTime;

        private bool _isInShowTime;
        private bool _isGameStarted;
        private bool _isInWaitStart;

        private int _matchCountNeeded;
        private int _matchCountHave;
        private int _comboCount;
        private int _winCount;
        private int _score;

        public void StartGame(int width, int height, bool isFirstTime)
        {
            if (isFirstTime)
            {
                _winCount = 0;
                _score = 0;
            }

            _comboCount = 0;

            _playTime = (int)(width * height * EACH_CARD_GAME_TIME);
            _restTime = (int)(width * height * EACH_CARD_REST_TIME);

            _startTime = 0;

            _matchCountNeeded = width * height / 2;
            _isInShowTime = true;
            _isGameStarted = false;
            _isInWaitStart = false;
            _matchCountHave = 0;
        }

        private void Update()
        {
            if (!_isInShowTime && !_isGameStarted && !_isInWaitStart) return;

            if (CheckShowTime()) return;
            if (CheckPreStartTime()) return;
            CheckGameLogic();
        }

        private bool CheckShowTime()
        {
            if (_isInShowTime)
            {
                _restTime -= Time.deltaTime;
                if (_restTime > 0)
                {
                    EventService.Invoke(GameEvents.ON_GAME_REST_TIME_CHANGED, (int)_restTime);
                    return true;
                }
                else
                {
                    _isInShowTime = false;
                    _isInWaitStart = true;
                    _startTime = 0;
                    EventService.Invoke(GameEvents.ON_HIDE_ALL_CARDS, true);
                }
            }

            return false;
        }

        private bool CheckPreStartTime()
        {
            if (_isInWaitStart)
            {
                _startTime += Time.deltaTime;
                if (_startTime > 1)
                {
                    _isInWaitStart = false;
                    _isGameStarted = true;
                    EventService.Invoke(GameEvents.ON_GAME_START, true);
                    _startTime = 0;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckGameLogic()
        {
            _playTime -= Time.deltaTime;
            if (_playTime > 0)
            {
                EventService.Invoke(GameEvents.ON_GAME_TIME_CHANGED, (int)_playTime);
            }
            else
            {
                _isInShowTime = false;
                _isGameStarted = false;
                int maxWinCount = StorageService.GetData(ConstDataNames.MAX_WIN_COUNT, 0);
                EventService.Invoke(GameEvents.ON_GAME_FINISHED, new OnGameFinished(false, _winCount, 0, _winCount > maxWinCount, _score));
                return;
            }

            if (_matchCountHave >= _matchCountNeeded)
            {
                _isInShowTime = false;
                _isGameStarted = false;
                _winCount++;

                int maxWinCount = StorageService.GetData(ConstDataNames.MAX_WIN_COUNT, 0);
                EventService.Invoke(GameEvents.ON_GAME_FINISHED, new OnGameFinished(true, _winCount, (int)_playTime, _winCount > maxWinCount, _score));
                return;
            }

            for (int i = 0; i < _selectedCards.Count; i++)
            {
                if (_selectedCards[i].IsAnySlotEmpty) continue;
                if (!_selectedCards[i].CanCheckCards) continue;

                if (_selectedCards[i].AreCardsSame)
                {
                    _selectedCards[i].SuccessSelect();
                    _matchCountHave++;
                    _comboCount++;
                    _score += _comboCount;
                }
                else
                {
                    _selectedCards[i].FailSelect();
                    _comboCount = 0;
                }

                EventService.Invoke(GameEvents.ON_SCORE_CHANGED, new OnScoreChanged(_score, _comboCount));

                _selectedCards.RemoveAt(i);
            }
        }

        public void SelectCard(Card card)
        {
            if (_selectedCards.Count > 0)
            {
                if (_selectedCards[^1].SelectCard(card))
                {
                    return;
                }
            }

            _selectedCards.Add(new SelectedCards(card));
        }
    }
}