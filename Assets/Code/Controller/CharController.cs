using System.Numerics;
using Code.Interfaces;
using Code.Player;
using Code.UserInput;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace Code.Controller
{
    internal class CharController : IInitialize, IExecute
    {
        private readonly IPlayer _player;
        private readonly IVRChecker _vrChecker;
        private readonly IUserInput _userInput;
        private readonly Config _config;

        private Vector3 _mousePosition;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;
        private bool _isMouseButton;

        public CharController(IPlayer player, Config config, IVRChecker vrChecker, IUserInput input)
        {
            _player = player;
            _config = config;
            _vrChecker = vrChecker;
            _userInput = input;
        }

        public void Initialize()
        {
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void OnMouseButtonUp(bool value) => _isMouseButtonUp = value;
        private void OnMouseButton(bool value) => _isMouseButton = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;


        public void Execute()
        {
            if (_vrChecker.IsVR)
            {
                ControlVR();
            }
            else
            {
                MouseControl();
            }
        }

        private void ControlVR()
        {
            Vector3 dir = _player.Rigidbody.velocity;

            if (_player.Transform.rotation.eulerAngles.z > _config.DeathZoneRotation
                && _player.Transform.rotation.eulerAngles.z <= 180)
            {
                dir.x = _player.Transform.rotation.eulerAngles.z * -1 * Time.deltaTime * _config.SideSpeedVR;
            }

            if (_player.Transform.rotation.eulerAngles.z > 180
                && _player.Transform.rotation.eulerAngles.z <= 360 - _config.DeathZoneRotation)
            {
                dir.x = _player.Transform.rotation.eulerAngles.z * -1 * Time.deltaTime * _config.SideSpeedVR;
            }

            dir.x = Input.GetAxis("Horizontal") * _config.SideSpeedVR;

            dir.z = _config.Speed;

            _player.Rigidbody.velocity = dir;
        }

        private void MouseControl()
        {
            Vector3 dir = _player.Rigidbody.velocity;
            if (_isMouseButtonDown)
            {
                _touchStartPosition = _mousePosition;
            }

            if (_isMouseButtonUp)
            {
                dir.x = (_mousePosition.normalized.x - _touchStartPosition.normalized.x) *
                        _config.SideSpeedMobile;
            }

            dir.z = _config.Speed;

            _player.Rigidbody.velocity = dir;
        }
    }

    
}