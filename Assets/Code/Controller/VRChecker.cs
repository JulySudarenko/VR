using UnityEngine;

namespace Code.Controller
{
    internal class VRChecker  : IVRChecker
    {
        public bool IsVR { get; }

        public VRChecker(Config config)
        {
            IsVR = PlayerPrefs.GetString(config.VRPrefs) == config.VRIsOn;
        }
    }
}
