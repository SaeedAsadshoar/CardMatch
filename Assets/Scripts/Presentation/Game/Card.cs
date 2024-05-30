using UnityEngine;

namespace Presentation.Game
{
    public class Card : MonoBehaviour
    {
        public void Inititialize(float x,float y)
        {
            transform.position = new Vector3(x, 0, y);
        }
    }
}