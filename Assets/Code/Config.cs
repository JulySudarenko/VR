using UnityEngine;


namespace Code
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config", order = 0)]
    public class Config : ScriptableObject
    {
        [Header("Player settings")] 
        public Transform vrCamera;
        public Transform camera;
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _sideSpeedVR = 2.0f;
        [SerializeField] private float _sideSpeedMobile = 200.0f;
        [SerializeField] private float _deathZoneRotation = 10.0f;

        [Header("Level setting")] 
        public Transform[] levelSegments;
        [SerializeField] private float _minDistance = 40.0f;

        [Header("Obstacles")] 
        public Transform[] obstacles;
        [SerializeField] private Vector2 _segmentWidth;
        [SerializeField] private float _spawnStep = 2.0f;
        [SerializeField] private float _spawnDistance = 30.0f;
        [SerializeField] private float _killDistanceZ = 40.0f;

        [Header("Spider")] 
        public Transform spider;
        [SerializeField] private float _spiderSpeed = 9.0f;

        [Header("Gameplay")] 
        [SerializeField] private string _winMessage = "You win!";
        [SerializeField] private string _loseMessage = "Game over";
        [SerializeField] private int _attackTime = 3000;
        [SerializeField] private int _gameTime = 500;

        [Header("VR Settings")] public GameObject UICamera;
        [SerializeField] private string _vrPrefs = "VRInfo";
        [SerializeField] private string _vrIsOn = "isOn";
        [SerializeField] private string _vrIsOff = "isOff";

        public string WinMessage => _winMessage;

        public string LoseMessage => _loseMessage;

        public string VRPrefs => _vrPrefs;

        public string VRIsOn => _vrIsOn;

        public string VRIsOff => _vrIsOff;

        public Vector2 SegmentWidth => _segmentWidth;

        public float Speed => _speed;

        public float SideSpeedVR => _sideSpeedVR;

        public float SideSpeedMobile => _sideSpeedMobile;

        public float DeathZoneRotation => _deathZoneRotation;

        public float MinDistance => _minDistance;

        public float SpawnStep => _spawnStep;

        public float SpawnDistance => _spawnDistance;

        public float KillDistanceZ => _killDistanceZ;

        public float SpiderSpeed => _spiderSpeed;

        public int AttackTime => _attackTime;

        public int GameTime => _gameTime;
    }
}
