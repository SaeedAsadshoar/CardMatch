using UnityEngine;

namespace Domain.Data
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Data/Settings", order = 0)]
    public class Settings : ScriptableObject
    {
        //use for show time at first of start game
        [SerializeField] private float _eachCardRestTime = 1;

        //use for calculate game time - width * height * eachCardShowTim - exmple : 3*3*5=45 sec whole level
        [SerializeField] private float _eachCardShowTime = 5;

        [SerializeField] private float _cardWidth = 1.5f;
        [SerializeField] private float _cardHeight = 1.5f;

        [SerializeField] private float _panelShowSpeed = 2f;
        [SerializeField] private float _panelHideSpeed = 3f;
        [SerializeField] private float _loadingsFillSpeed = 1f;


        public float EachCardRestTime => _eachCardRestTime;
        public float EachCardShowTime => _eachCardShowTime;
        public float CardWidth => _cardWidth;
        public float CardHeight => _cardHeight;
        public float PanelShowSpeed => _panelShowSpeed;
        public float PanelHideSpeed => _panelHideSpeed;
        public float LoadingsFillSpeed => _loadingsFillSpeed;
    }
}