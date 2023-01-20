using System;
using Code.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Controller
{
    internal class ViewController : IInitialize, IDisposable
    {
        private readonly IEndGame _endGame;
        private readonly IVRChecker _vrChecker;
        private readonly Config _config;
        private readonly GameView _gameView;
        private readonly GameView _gameViewVR;

        public ViewController(Config config, GameView gameView, GameView gameViewVR, IEndGame endGame,
            IVRChecker vrChecker)
        {
            _config = config;
            _gameView = gameView;
            _gameViewVR = gameViewVR;
            _endGame = endGame;
            _vrChecker = vrChecker;
        }

        public void Initialize()
        {
            if (_vrChecker.IsVR)
            {
                _gameViewVR.Restart.onClick.AddListener(() => LoadScene(1));
                _gameViewVR.Exit.onClick.AddListener(() => LoadScene(0));
                _gameViewVR.Init(_endGame, _config);
                _gameView.gameObject.SetActive(false);
            }
            else
            {
                _gameView.Restart.onClick.AddListener(() => LoadScene(1));
                _gameView.Exit.onClick.AddListener(() => LoadScene(0));
                _gameView.Init(_endGame, _config);
                _gameViewVR.gameObject.SetActive(false);
            }
        }

        private void LoadScene(int num)
        {
            SceneManager.LoadScene(num);
        }

        public void Dispose()
        {
            if (_vrChecker.IsVR)
            {
                _gameViewVR.Restart.onClick.RemoveListener(() => LoadScene(1));
                _gameViewVR.Exit.onClick.RemoveListener(() => LoadScene(0));
            }
            else
            {
                _gameView.Restart.onClick.RemoveListener(() => LoadScene(1));
                _gameView.Exit.onClick.RemoveListener(() => LoadScene(0));
            }
        }
    }
}
