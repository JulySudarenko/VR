using System;
using GeekBrains.Helpers;
using UnityEngine;

namespace GeekBrains
{
	public abstract class Weapons : BaseObjectScene
	{
		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 1000;
		[SerializeField] protected float _rechargeTime = 0.2f;

        [SerializeField] public Socket magSocket;

		protected Timer _timer = new Timer();
		protected bool _isFire = true;

		[HideInInspector] public Ammunition Ammunition;

		public abstract void Fire();

		protected void Update()
		{
			//if(!IsVisible && _isFire) return;
			_timer.Update();
			if (_timer.IsEvent())
			{
				_isFire = true;
			}
		}
	}
}