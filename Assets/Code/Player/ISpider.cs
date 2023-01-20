using UnityEngine;

namespace Code.Player
{
    internal interface ISpider
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        Animator Animator { get; }
        IHit Trigger { get; }
        int ColliderID { get; }
    }
}
