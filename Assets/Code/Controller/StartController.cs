using Code.Player;
using Code.UserInput;
using UnityEngine;


namespace Code.Controller
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private Config _config;
        [SerializeField] private GameView _gameViewVR;
        [SerializeField] private GameView _gameView;

        private Controllers _controller;

        private void Awake()
        {
            _controller = new Controllers();
            
            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);
            
            IVRChecker vrChecker = new VRChecker(_config);
            IFactory factory = new PlayerFactory(_config, vrChecker);
            var player = new Player.Player(factory);
            var charController = new CharController(player, _config, vrChecker, input);
            
            var folder = new GameObject("Root").transform;
            var spider = new Spider(_config, folder);
            var spiderController = new SpiderController(player, spider, _config);
            var gameController = new EndGameController(player, spider, _config, vrChecker);
            var levelGenerator = new LevelGenerator(_config, player, gameController, folder);
            var obstacleSpawner = new ObstacleSpawner(_config, player, folder);
            var obstacleKiller = new ObstacleKiller(obstacleSpawner, _config, player, gameController);
            var viewController = new ViewController(_config, _gameView, _gameViewVR, gameController, vrChecker);

            _controller.Add(charController);
            _controller.Add(inputController);
            _controller.Add(levelGenerator);
            _controller.Add(obstacleSpawner);
            _controller.Add(obstacleKiller);
            _controller.Add(spiderController);
            _controller.Add(gameController);
            _controller.Add(viewController);
        }

        private void Start()
        {
            _controller.Initialize();
        }

        private void Update()
        {
            _controller.Execute();
        }
    }
}
