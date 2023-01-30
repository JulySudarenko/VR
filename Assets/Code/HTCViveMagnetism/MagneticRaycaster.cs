using System;
using System.Runtime.ExceptionServices;
using HTC.UnityPlugin.Pointer3D;
using HTC.UnityPlugin.Vive.SteamVRExtension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

namespace Code.HTCViveMagnetism
{
    public class MagneticRaycaster : MonoBehaviour
    {
        public enum TypeOfMagnet
        {
            Blue = 0,
            Red = 1
        }

        [SerializeField] private TypeOfMagnet _coloTypeOfMagnet;

        [Tooltip("Ссылка на spell персонажа")] [SerializeField]
        private CharMagnetic refToChar;

        private RaycastResult _curObj;
        private Pointer3DRaycaster _raycaster;

        private void OnValidate()
        {
            _raycaster = GetComponent<Pointer3DRaycaster>();
        }

        private void LateUpdate()
        {
            Raycasting();
        }

        private void Raycasting()
        {
            _curObj = _raycaster.FirstRaycastResult();
        }

        public void StartMagnet()
        {
            if (_curObj.isValid)
            {
                Rigidbody rg = _curObj.gameObject.GetComponent<Rigidbody>();
                switch ((int) _coloTypeOfMagnet)
                {
                    case 0:
                        if (rg != null)
                        {
                            refToChar.SetBlue(_curObj.gameObject.transform);
                        }
                        else
                        {
                            refToChar.SetBlue(_curObj.worldPosition);
                        }

                        break;
                    case 1:
                        if (rg != null)
                        {
                            refToChar.SetRed(_curObj.gameObject.transform);
                        }
                        else
                        {
                            refToChar.SetRed(_curObj.worldPosition);
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
