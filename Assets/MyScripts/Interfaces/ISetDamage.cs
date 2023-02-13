namespace GeekBrains
{
	public interface ISetDamage
	{
		void SetHp(BulletCollisonInfo info);
		float Hp { get; set; }
	}
}
