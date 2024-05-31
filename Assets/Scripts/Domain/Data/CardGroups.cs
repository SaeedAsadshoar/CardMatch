using System;
using UnityEngine;

namespace Domain.Data
{
    [CreateAssetMenu(fileName = "CardGroups", menuName = "Data/CardGroups", order = 0)]
    public class CardGroups : ScriptableObject
    {
        [SerializeField] private CardGroup[] _cardGroups;

        public CardGroup[] AllCardGroups => _cardGroups;
    }

    [Serializable]
    public struct CardGroup
    {
        [SerializeField] private string _name;
        [SerializeField] private Texture2D _thumb;
        [SerializeField] private string _key;

        public string Name => _name;
        public Texture2D Thumb => _thumb;
        public string Key => _key;
    }
}