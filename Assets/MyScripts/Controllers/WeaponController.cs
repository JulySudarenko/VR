using System;
using GeekBrains.Helpers;
using UnityEngine;

namespace GeekBrains.Controllers
{
	public sealed class WeaponController : BaseController
	{
        //private Hand hand;
        private Weapons _weapons;
		private Camera _main;
		private Vector3 _centr;
		public int MouseButton { get; private set; }
		public event EventHandler End;

		private void Start()
		{
			MouseButton = (int)Helpers.MouseButton.LeftButton;
			_main = Camera.main;
			_centr = new Vector3(Screen.width / 2, Screen.height / 2);
		}       

        private void Update()
        {
           // if (Input.GetButtonDown("HTC_VIU_LeftTrigger") || Input.GetButtonDown("HTC_VIU_RightTrigger"))
          //      Fire();

            /*
			if(!Enable) return;
			if (Input.GetMouseButton(MouseButton)) // Pew Pew
			{
				if (_weapons != null)
				{
					_weapons.Fire(_ammunition);
					RaycastHit hit;
					Ray ray = _main.ScreenPointToRay(_centr);
					if (Physics.Raycast(ray, out hit))
					{
						if(hit.distance <= 3) return;
						_weapons.transform.LookAt(hit.point);
					}
				}
			}
			if (Input.GetMouseButtonUp(MouseButton))
			{
				if (End != null) End.Invoke(this, EventArgs.Empty);
			}
            */
        }

        public void Fire()
        {
            print("pew pew");

            if (!Enable) return;
            print("pew pew");

            if (_weapons != null)
            {
                _weapons.Fire();
                RaycastHit hit;
                Ray ray = _main.ScreenPointToRay(_centr);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.distance <= 3) return;
                    _weapons.transform.LookAt(hit.point);
                }
            }
        }

		public void On(Weapons weapons) // Взять пистулю
		{
			if (Enable) return;
			base.On();
			_weapons = weapons;
			_weapons.IsVisible = true;
		}

		public override void Off() // Положить пистулю
		{
			if (!Enable) return;
			base.Off();
			_weapons.IsVisible = false;
			_weapons = null;
		}
	}
}