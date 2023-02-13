using GeekBrains;
using UnityEngine;

public class Target : BaseObjectScene
{
	private bool _isDead;
	public float Hp { get; set; }

	private void Start()
	{
		Hp = 100;
	}

	public void SetHp(float damage)
	{
		if (_isDead) return;
		if (Hp > 0)
		{
			Hp -= damage;
		}

		if (Hp <= 0)
		{
			_isDead = true;
			Color = Color.red;
			IsVisible = false;
			Destroy(InstanceObject, 5);
		}
	}
}