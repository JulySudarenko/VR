using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.HTCViveMagnetism
{
    public class Centering : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private CapsuleCollider _myCol;

        private Vector3 _vector;

        private void OnValidate()
        {
            _myCol = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            FindTeleportPivotAndTarget();
            _vector.y = _myCol.center.y;
        }

        private void Update()
        {
            _vector.x = _pivot.localPosition.x;
            _vector.z = _pivot.localPosition.z;

            _myCol.center = _vector;
        }


        private void FindTeleportPivotAndTarget()
        {
            foreach (var camera in Camera.allCameras)
            {
                if (!camera.enabled)
                {
                    continue;
                }

                if (camera.stereoTargetEye != StereoTargetEyeMask.Both)
                {
                    continue;
                }

                _pivot = camera.transform;
            }
        }
    }


}
