using HTC.UnityPlugin.Vive;
using UnityEngine;
using Valve.VR;

namespace Code.HTCViveMagnetism
{
    public class TrackPadScroller : MonoBehaviour
    {
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _deadZone = 0.1f;

        private SteamVR_RenderModel _vive;
        private CharMagnetic _magnet;

        private void Start()
        {
            _magnet = GetComponent<CharMagnetic>();
        }

        private void Update()
        {
            if (_vive == null)
                _vive = GetComponentInChildren<SteamVR_RenderModel>();
            
            float dpR = ViveInput.GetPadTouchDelta(HandRole.RightHand).y;
            if (Mathf.Abs(dpR) > _deadZone)
            {
                _magnet.ChangeSpringPower(dpR * _speed);
                _vive.controllerModeState.bScrollWheelVisible = true;
            }
            
            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.PadTouch))
                _vive.controllerModeState.bScrollWheelVisible = false;
            
            float dpL = ViveInput.GetPadTouchDelta(HandRole.LeftHand).y;
            if (Mathf.Abs(dpL) > _deadZone)
            {
                _magnet.ChangeSpringPower(dpL * _speed);
                _vive.controllerModeState.bScrollWheelVisible = true;
            }
            
            if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.PadTouch))
                _vive.controllerModeState.bScrollWheelVisible = false;
        }
    }
}
