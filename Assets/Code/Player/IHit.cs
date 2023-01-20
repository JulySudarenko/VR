using System;

namespace Code.Player
{
    internal interface IHit
    {
        event Action<int> OnHitEnter;
    }
}
