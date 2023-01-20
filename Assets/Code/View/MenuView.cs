using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _startRunner;
        [SerializeField] private Button _exitGame;
        [SerializeField] private Toggle _isVR;

        public Button StartRunner => _startRunner;

        public Button ExitGame => _exitGame;

        public Toggle IsVR => _isVR;
    }
}
