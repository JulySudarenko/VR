using System.Linq;
using UnityEngine;

namespace GeekBrains.Controllers
{
	public sealed class InputController : BaseController
	{
		public KeyCode _activeLight = KeyCode.F;
		private int _indexWeapons;

		private void Start()
		{
            /*
			var temp = InputKeyRepositoty.ListInputKeys;
			foreach (var inputKey in temp)
			{
				if (inputKey.Name == "Flashlight")
				{
					_activeLight = inputKey.KeyCode;
				}
			}
            */
		}

		private void Update()
		{
			if (Input.GetKeyDown(_activeLight))
			{
				Main.Instance.FlashLighController.Switch();
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SetWeapon(0);
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				if (_indexWeapons < Main.Instance.ObjectManager.Weapons.Length - 1)
				{
					_indexWeapons++;
				}
				else
				{
					_indexWeapons = -1;
				}
				SetWeapon(_indexWeapons);
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				if (_indexWeapons <= 0)
				{
					_indexWeapons = Main.Instance.ObjectManager.Weapons.Length;
				}
				else
				{
					_indexWeapons--;
				}
				SetWeapon(_indexWeapons);
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Main.Instance.WeaponController.Off();
				Main.Instance.FlashLighController.Off();
			}
		}

		private void SetWeapon(int indexWeapon)
		{
			Main.Instance.WeaponController.Off();
			if (indexWeapon < 0 || indexWeapon >= Main.Instance.ObjectManager.Weapons.Length) return;
			var tempWeapon = Main.Instance.ObjectManager.Weapons[indexWeapon];
			if (!tempWeapon) return;
			Main.Instance.WeaponController.On(tempWeapon);
		}
	}
}