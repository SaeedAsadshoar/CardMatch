using UnityEngine;

namespace Presentation.Game.Card
{
    public class CardTheme : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _themeRenderers;
        [SerializeField] private MeshRenderer _cardRenderer;

        public void LoadTheme(Texture2D theme, Texture2D card)
        {
            foreach (var themeRenderer in _themeRenderers)
            {
                themeRenderer.sharedMaterial.mainTexture = theme;
            }

            _cardRenderer.material.mainTexture = card;
        }
    }
}