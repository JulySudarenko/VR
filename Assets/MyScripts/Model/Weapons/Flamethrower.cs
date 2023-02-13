using System;
using UnityEngine;

namespace GeekBrains
{
	public class Flamethrower : Weapons
	{
		[SerializeField] private GameObject _fire;

		private void Start()
		{
			_fire.gameObject.SetActive(false);
			//Ammunition = _fire.GetComponent<Ammunition>();

		}

		public override void Fire()
		{
			if (_isFire)
			{
				//(ammunition as Fire).Active(true);
				_fire.gameObject.SetActive(true);
			}
		}

		public void End(object obj, EventArgs args)
		{
			//(ammunition as Fire).Active(false);
			_fire.gameObject.SetActive(false);
		}

	}
}