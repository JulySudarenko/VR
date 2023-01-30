using System.Collections;
using HTC.UnityPlugin.Vive;
using UnityEngine;

namespace Code.HTCViveMagnetism
{
    public class MyTeleport : Teleportable
    {
        [SerializeField] private float _speed;

        [SerializeField] private float _coolDown;

        public IEnumerator StartTeleport(Vector3 position, float duration)
        {
            while (true)
            {
                target.position = Vector3.MoveTowards(target.position, position, _speed * Time.deltaTime);

                Vector3 v = position;
                v.y = target.position.y;

                if (Vector3.Distance(target.position, v) < 0.1f)
                {
                    yield return new WaitForSeconds(_coolDown);
                    teleportCoroutine = null;
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
