using UnityEngine;

namespace GeekBrains
{
	public abstract class Ammunition : BaseObjectScene
	{
		[SerializeField] protected float _timeToDestruct = 10;
		[SerializeField] protected float _baseDamage = 10;

		protected float _currentDamage;
		public bool IsDamage = true;

		public float CurrentDamage
		{
			get { return _currentDamage; }
		}

		protected override void Awake()
		{
			base.Awake();
			Destroy(InstanceObject, _timeToDestruct);
			if (IsDamage)
			{
				_currentDamage = _baseDamage;
			}
		}
	}
}