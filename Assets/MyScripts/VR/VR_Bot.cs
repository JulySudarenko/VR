using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

namespace GeekBrains
{
    public class VR_Bot : Bot
    {
        override protected void Start()
        {
            _patrol = new Patrol();
            Target = FindObjectOfType<Player>().headCollider.transform; 

            if (_headBot != null) _headBot.HeadShot = SetHp;
        }
    }
}