using System;
using UnityEngine;

namespace Code.Player
{
    internal class TriggerHit : MonoBehaviour, IHit
    {
        public event Action<int> OnHitEnter;

        public void OnTriggerEnter(Collider other)
        {
            OnHitEnter?.Invoke(other.gameObject.GetInstanceID());
        }
    }
}
