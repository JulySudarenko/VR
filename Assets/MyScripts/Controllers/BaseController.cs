using UnityEngine;

namespace GeekBrains.Controllers
{
	public abstract class BaseController : MonoBehaviour
	{
		protected bool Enable { get; private set; }

		public virtual void On()
		{
			Enable = true;
		}

		public virtual void Off()
		{
			Enable = false;
		}
	}
}
