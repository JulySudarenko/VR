using UnityEngine;

namespace GeekBrains
{
	public class Fire : Ammunition
	{
		private Camera _main;
		private Vector3 _centr;
		[SerializeField] private FlamesSO _flamesSo;
		public ParticleSystem[] ParticleSystems;

		public void Active(bool value)
		{
			if (value)
			{
				foreach (var particleSystem in ParticleSystems)
				{
					particleSystem.Play();
				}
			}
			else
			{
				foreach (var particleSystem in ParticleSystems)
				{
					particleSystem.Stop();
				}
			}
		}

		private void Start()
		{
			_flamesSo.Start();
			_main = Camera.main;
			_centr = new Vector3(Screen.width/2, Screen.height / 2);
		}
		
		private void OnParticleCollision(GameObject other)
		{
			SetDamage(other.GetComponent<ISetDamage>());
			if (Random.Range(0, 100) < 20)
			{
				CreateEffects(other.transform);
			}
		}

		private void SetDamage(ISetDamage obj)
		{
			if(obj == null) return;
			obj.SetHp(new BulletCollisonInfo{Damage = _flamesSo.Damage });
		}

		private void CreateEffects(Transform obj)
		{
			RaycastHit hit;
			Ray ray = _main.ScreenPointToRay(_centr);
			if (Physics.Raycast(ray, out hit))
			{
				_flamesSo.EnableFlames(hit.point,
					Quaternion.LookRotation(hit.point, hit.normal), obj);
			}
		}
	}
}