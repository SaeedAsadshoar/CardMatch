using UnityEngine;

namespace Domain.Data
{
    [CreateAssetMenu(fileName = "Cards", menuName = "Data/Cards", order = 0)]
    public class Cards : ScriptableObject
    {
        [SerializeField] private Texture2D[] _textures;

        public Texture2D[] Textures => _textures;
    }
}