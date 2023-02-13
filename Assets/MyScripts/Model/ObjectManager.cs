using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace GeekBrains
{
	public sealed class ObjectManager : MonoBehaviour
	{
		private Weapons[] _weapons = new Weapons[5];
		private FirstPersonController _firstPersonController;

		public Weapons[] Weapons
		{
			get
			{
				return _weapons;
			}
		}

		private void Start()
		{
			_firstPersonController = FindObjectOfType<FirstPersonController>();

			if (_firstPersonController != null) _weapons = _firstPersonController.GetComponentsInChildren<Weapons>();
			foreach (var weapon in _weapons)
			{
				if (weapon != null)
				{
					weapon.IsVisible = false;
					if (weapon is Flamethrower)
					{
						Main.Instance.WeaponController.End += (weapon as Flamethrower).End;
					}
				}
			}
			if (Weapons.Length >= 1)
			{
				var tempGun = Weapons[0];
				if (tempGun)
				{
					Main.Instance.WeaponController.On(tempGun);
				}
			}
		}
	}
}