using Code.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exit;
        [SerializeField] private Text _finalText;

        private IEndGame _endGame;
        private Config _config;

        public Button Restart => _restart;
        public Button Exit => _exit;

        public void Init(IEndGame endGame, Config config)
        {
            _endGame = endGame;
            _config = config;
            HideUI();
            _endGame.EndGame += ShowUI;
        }

        private void HideUI()
        {
            _restart.gameObject.SetActive(false);
            _exit.gameObject.SetActive(false);
            _finalText.gameObject.SetActive(false);
        }

        private void ShowUI(bool value)
        {
            _finalText.text = value ? _config.WinMessage : _config.LoseMessage;
            _restart.gameObject.SetActive(true);
            _exit.gameObject.SetActive(true);
            _finalText.gameObject.SetActive(true);
        }
    }
}
