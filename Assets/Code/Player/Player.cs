using Code.Assistance;
using UnityEngine;


namespace Code.Player
{
    internal class Player : IPlayer
    {
        public Transform Transform { get; }
        public Rigidbody Rigidbody { get; }
        public int ColliderID { get; }

        public Player(IFactory factory)
        {
            var player = factory.Create();
            Transform = player.transform;
            Rigidbody = player.GetOrAddComponent<Rigidbody>();
            var collider = player.GetOrAddComponent<Collider>();
            ColliderID = player.GetInstanceID();
        }
    }
}
