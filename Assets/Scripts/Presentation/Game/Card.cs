using UnityEngine;

namespace Presentation.Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _themeRenderers;
        [SerializeField] private MeshRenderer _cardRenderer;
        private string _id;

        public void Initialize(Texture2D theme, Texture2D card, float x, float y)
        {
            transform.position = new Vector3(x, 0, y);

            foreach (var themeRenderer in _themeRenderers)
            {
                themeRenderer.sharedMaterial.mainTexture = theme;
            }

            _cardRenderer.material.mainTexture = card;
            _id = card.name;
        }
    }
}