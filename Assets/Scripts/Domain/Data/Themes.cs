using System;
using UnityEngine;

namespace Domain.Data
{
    [CreateAssetMenu(fileName = "Themes", menuName = "Data/Themes", order = 0)]
    public class Themes : ScriptableObject
    {
        [SerializeField] private Theme[] _themes;

        public Theme[] AllThemes => _themes;
    }

    [Serializable]
    public struct Theme
    {
        [SerializeField] private string _name;
        [SerializeField] private Texture2D _thumb;

        public string Name => _name;
        public Texture2D Thumb => _thumb;
    }
}