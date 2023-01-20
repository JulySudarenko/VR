using Code.View;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Code.Controller
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Config _config;
        [SerializeField] private MenuView _menuView;
        [SerializeField] private MenuView _menuViewVR;
        [SerializeField] private GameObject _vrCamera;

        private Camera _camera;
        // [SerializeField] private Button _startRunner;
        // [SerializeField] private Button _exitGame;
        // [SerializeField] private Toggle _isVR;

        private void Start()
        {
            _camera = Camera.main;
            CheckValue();
        }

        private void CheckValue()
        {
            if (PlayerPrefs.HasKey(_config.VRPrefs))
            {
                string flag = PlayerPrefs.GetString(_config.VRPrefs);
                if (flag == _config.VRIsOn)
                {
                    ActivateVR();
                }
                else
                {
                    ActivateMobile();
                }
            }
            else
            {
                PlayerPrefs.SetString(_config.VRPrefs, _config.VRIsOff);
                ActivateMobile();
            }
        }

        private void ActivateVR()
        {
            _menuViewVR.IsVR.isOn = true;
            _menuViewVR.gameObject.SetActive(true);
            _menuView.gameObject.SetActive(false);
            _menuViewVR.StartRunner.onClick.AddListener(() => LoadScene(1));
            _menuViewVR.ExitGame.onClick.AddListener(ExitGame);
            _menuViewVR.IsVR.onValueChanged.AddListener(SetInfo);
            _vrCamera.SetActive(true);
            _camera.gameObject.SetActive(false);
        }

        private void ActivateMobile()
        {
            _menuViewVR.gameObject.SetActive(false);
            _menuView.gameObject.SetActive(true);
            _menuView.StartRunner.onClick.AddListener(() => LoadScene(1));
            _menuView.ExitGame.onClick.AddListener(ExitGame);
            _menuView.IsVR.onValueChanged.AddListener(SetInfo);
            _menuView.IsVR.isOn = false;
            _vrCamera.SetActive(false);
            _camera.gameObject.SetActive(true);
        }

        private void SetInfo(bool value)
        {
            PlayerPrefs.SetString(_config.VRPrefs, value ? _config.VRIsOn : _config.VRIsOff);
            //SceneManager.LoadScene(0);
        }

        private void LoadScene(int num)
        {
            SceneManager.LoadScene(num);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void RemoveListeners(bool value)
        {
            if (value)
            {
                _menuView.StartRunner.onClick.RemoveListener(() => LoadScene(1));
                _menuView.ExitGame.onClick.RemoveListener(ExitGame);
                _menuView.IsVR.onValueChanged.RemoveListener(SetInfo);
            }
            else
            {
                _menuViewVR.StartRunner.onClick.RemoveListener(() => LoadScene(1));
                _menuViewVR.ExitGame.onClick.RemoveListener(ExitGame);
                _menuViewVR.IsVR.onValueChanged.RemoveListener(SetInfo);
            }
        }

        ~GameMenu()
        {
            if (PlayerPrefs.HasKey(_config.VRPrefs))
            {
                string flag = PlayerPrefs.GetString(_config.VRPrefs);
                RemoveListeners(flag == _config.VRIsOn);
            }
            else
            {
                RemoveListeners(false);
            }
        }
    }
}
