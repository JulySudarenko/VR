using Code.Controller;
using UnityEngine;

namespace Code.Player
{
    internal class PlayerFactory : IFactory
    {
        private readonly Config _config;
        private readonly IVRChecker _checker;

        public PlayerFactory(Config config, IVRChecker checker)
        {
            _config = config;
            _checker = checker;
        }

        public GameObject Create()
        {
            return _checker.IsVR
                ? Object.Instantiate(_config.vrCamera).gameObject
                : Object.Instantiate(_config.camera).gameObject;
        }
    }
}
