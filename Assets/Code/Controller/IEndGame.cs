using System;

namespace Code.Controller
{
    public interface IEndGame
    {
        event Action<bool> EndGame;
    }
}
