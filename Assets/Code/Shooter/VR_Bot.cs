using GeekBrains;

namespace Code.Shooter
{
    public class VR_Bot : Bot
    {
        override protected void Start()
        {
            _patrol = new Patrol();
            Target = FindObjectOfType<Valve.VR.InteractionSystem.Player>().headCollider.transform;

            if (_headBot != null) _headBot.HeadShot = SetHp;
        }
    }
}
