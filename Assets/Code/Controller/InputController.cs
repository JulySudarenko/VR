using Code.Interfaces;
using Code.UserInput;

namespace Code.Controller
{
    public sealed class InputController : IExecute
    {
        private readonly IUserInput _userInput;

        public InputController(IUserInput userInput)
        {
            _userInput = userInput;
        }
        
        public void Execute()
        {
            _userInput.GetTouchDown();
            _userInput.GetTouchUp();
            _userInput.GetTouch();
        }
        
    }
}
