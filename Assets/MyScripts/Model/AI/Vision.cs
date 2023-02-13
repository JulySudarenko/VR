
using UnityEngine;

namespace GeekBrains
{
    [System.Serializable]
    public class Vision
    {
        public float ActivDist = 15;
        public float ActivAngle = 45;

        public bool VisionMaht(Transform player, Transform target)
        {
            return Dist(player, target) && Angle(player, target) && !CheckBloked(player, target);
        }

        private bool CheckBloked(Transform player, Transform target)
        {
            RaycastHit hit;
            if (!Physics.Linecast(player.position, target.position, out hit)) return true;
            return hit.transform != target;
        }

        public bool Dist(Transform player, Transform target)
        {
            var dis = Vector3.Distance(player.position, target.position);
            return dis <= ActivDist;
        }

        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActivAngle;
        }
    }
}