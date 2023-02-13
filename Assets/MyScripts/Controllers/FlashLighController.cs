namespace GeekBrains.Controllers
{
	public class FlashLighController : BaseController
	{
		private LightUser _lightUser;

		private void Start()
		{
			_lightUser = FindObjectOfType<LightUser>();
		}

		private void Update()
		{
			if(!Enable) return;
		}

		public override void On()
		{
			if(Enable) return;
			base.On();
			_lightUser.Switch(true);
		}

		public override void Off()
		{
			if (!Enable) return;
			base.Off();
			_lightUser.Switch(false);
		}

		public void Switch()
		{
			if (Enable)
			{
				Off();
			}
			else
			{
				On();
			}
		}
	}
}