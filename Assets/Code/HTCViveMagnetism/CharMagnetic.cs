using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Code.HTCViveMagnetism
{
    [System.Serializable]
    public struct MagneticPoint
    {
        public List<SpringJoint> jointList;
        public List<Rigidbody> rg;
        public List<ParticleSystem> highLight;
        public Transform blueObj;
        public Transform redObj;
        public Vector3 bluePos;
        public Vector3 redPos;
    }

    public class CharMagnetic : MonoBehaviour
    {
        [SerializeField] private float _spellDistance = 20.0f;
        [SerializeField] private float _maxMagneticForce = 50.0f;
        [SerializeField] private MagneticPoint _magneticSpell;
        [SerializeField] private Transform _blueHolder;
        [SerializeField] private Transform _redHolder;
        [SerializeField] private Material _redMat;
        [SerializeField] private Material _blueMat;
        [SerializeField] private Material _yellowMat;
        [SerializeField] private ParticleSystem _hlReference;

        public void SetBlue(Transform trans)
        {
            _magneticSpell.blueObj = trans;
            _magneticSpell.bluePos = trans.position;
            Highlighting(true, trans);
            CheckToJoint();
        }

        private void Highlighting(bool isBlue, Transform trans)
        {
            ParticleSystem ps = Instantiate(_hlReference, trans, false);

            if (isBlue)
            {
                ps.GetComponent<Renderer>().material = _blueMat;
            }
            else
            {
                ps.GetComponent<Renderer>().material = _redMat;
            }

            _magneticSpell.highLight.Add(ps);
        }

        public void SetRed(Transform trans)
        {
            _magneticSpell.redObj = trans;
            _magneticSpell.redPos = trans.position;
            Highlighting(false, trans);
            CheckToJoint();
        }

        public void SetBlue(Vector3 trans)
        {
            _magneticSpell.blueObj = _blueHolder;
            _magneticSpell.bluePos = trans;
            _blueHolder.position = trans;
            _blueHolder.GetChild(0).gameObject.SetActive(true);
            CheckToJoint();
        }

        public void SetRed(Vector3 trans)
        {
            _magneticSpell.redObj = _blueHolder;
            _magneticSpell.redPos = trans;
            _blueHolder.position = trans;
            _blueHolder.GetChild(0).gameObject.SetActive(true);
            CheckToJoint();
        }

        private void CheckToJoint()
        {
            if (_magneticSpell.blueObj != null && _magneticSpell.redObj != null)
            {
                if (Vector3.Distance(_magneticSpell.redPos, _magneticSpell.bluePos) < _spellDistance) CreateJoint();
                else EreaseSpell();
            }
        }

        private void EreaseSpell()
        {
            _magneticSpell.blueObj = null;
            _magneticSpell.redObj = null;

            for (int i = 0; i < _magneticSpell.highLight.Count; i++)
            {
                _magneticSpell.highLight[i].GetComponent<Renderer>().material = _yellowMat;
            }
        }

        private void CreateJoint()
        {
            SpringJoint sp = _magneticSpell.blueObj.gameObject.AddComponent<SpringJoint>();
            sp.autoConfigureConnectedAnchor = false;
            sp.anchor = Vector3.zero;
            sp.connectedAnchor = Vector3.zero;
            sp.enableCollision = true;
            sp.enablePreprocessing = false;
            sp.connectedBody = _magneticSpell.redObj.GetComponent<Rigidbody>();

            EreaseSpell();
            _magneticSpell.jointList.Add(sp);
            Rigidbody rg = sp.GetComponent<Rigidbody>();
            _magneticSpell.rg.Add(rg);
            AddRG(sp.connectedBody);
        }

        private void AddRG(Rigidbody RG)
        {
            if (_magneticSpell.rg == null)
            {
                return;
            }

            for (int i = 0; i < _magneticSpell.rg.Count; i++)
            {
                if (RG == _magneticSpell.rg[i])
                    break;
                if (i == _magneticSpell.rg.Count - 1)
                {
                    _magneticSpell.rg.Add(RG);
                }

                break;
            }
        }

        public void DestroyAllJoints()
        {
            for (int i = 0; i < _magneticSpell.highLight.Count; i++)
            {
                Destroy(_magneticSpell.jointList[i]);
            }

            for (int i = 0; i < _magneticSpell.rg.Count; i++)
            {
                _magneticSpell.rg[i].angularDrag = 0.05f;
                _magneticSpell.rg[i].drag = 0;
                _magneticSpell.rg[i].WakeUp();
            }

            _magneticSpell.jointList.Clear();
            _magneticSpell.rg.Clear();
            EreaseSpell();

            for (int i = 0; i < _magneticSpell.highLight.Count; i++)
            {
                Destroy(_magneticSpell.highLight[i]);
            }

            _magneticSpell.highLight.Clear();
            DisableHolders();
        }

        private void DisableHolders()
        {
            _blueHolder.GetChild(0).gameObject.SetActive(false);
            _redHolder.GetChild(0).gameObject.SetActive(false);
        }

        public void ChangeSpringPower(float fNum)
        {
            if (_magneticSpell.jointList.Count > 0)
            {
                for (int i = 0; i < _magneticSpell.jointList.Count; i++)
                {
                    _magneticSpell.jointList[i].spring += fNum;
                    _magneticSpell.jointList[i].damper += fNum;
                    _magneticSpell.jointList[i].damper +=
                        Mathf.Clamp(_magneticSpell.jointList[i].damper, 0, _maxMagneticForce);
                    _magneticSpell.jointList[i].spring +=
                        Mathf.Clamp(_magneticSpell.jointList[i].spring, 0, _maxMagneticForce);
                }

                for (int i = 0; i < _magneticSpell.rg.Count; i++)
                {
                    _magneticSpell.rg[i].WakeUp();
                    _magneticSpell.rg[i].angularDrag += fNum;
                    _magneticSpell.rg[i].drag += fNum;
                    _magneticSpell.rg[i].angularDrag =
                        Mathf.Clamp(_magneticSpell.rg[i].angularDrag, 0, _maxMagneticForce);
                    _magneticSpell.rg[i].drag = Mathf.Clamp(_magneticSpell.rg[i].drag, 0, _maxMagneticForce);
                }
            }
        }
    }
}
