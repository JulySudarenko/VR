using UnityEngine;

namespace Code.Player
{
    internal interface IPlayer
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        int ColliderID { get; }
    }
}
