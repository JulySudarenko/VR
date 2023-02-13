using System;

namespace GeekBrains
{
	public class HeadBot : BaseObjectScene, ISetDamage
	{
		public delegate void SetHpFromPart(BulletCollisonInfo info);

		Nullable<int> x﻿;
		//int? x﻿;

		public SetHpFromPart HeadShot { get; set; }

		public float Hp { get; set; }

		public void SetHp(BulletCollisonInfo info)
		{
			var damege = info.Damage * 10;
			if (HeadShot != null) HeadShot.Invoke(new BulletCollisonInfo{Damage = damege });

			x﻿ = null;
		}
	}
}