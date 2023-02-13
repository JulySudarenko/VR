using UnityEngine;
using Valve.VR.InteractionSystem;

namespace GeekBrains
{
	public class Gun : Weapons
	{
        [SerializeField] private bool _isRaycast;
        public WeaponType.magazType TypeOfGun = WeaponType.magazType.pistol;
		private Camera _main;
		private Vector3 _centr;
		[SerializeField] private float _damage = 10;

		private void Start()
		{
            Ammunition = Resources.Load<Bullet>("Bullet");
			if (_isRaycast)
			{
				Ammunition.IsDamage = false;
			}
			_main = Camera.main;
			_centr = new Vector3(Screen.width / 2, Screen.height / 2);
		}       

        private void HandAttachedUpdate(Hand hand)
        {
            if (hand.controller.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                Fire();
            }

            if (hand.controller.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                magSocket.Release();
            }
        }

        public override void Fire()
		{
			if (!Ammunition) return;
			if (_isFire)
			{
                // Проверка есть ли патроны в магазине
                if (magSocket.mag == null) return;
                if (magSocket.mag.ammo <= 0)
                {
                    magSocket.mag.Bullet.SetActive(false); // отключение бутофорной пули
                    return;
                }

                magSocket.mag.ammo--;

				var tempBullet = Instantiate(Ammunition, _barrel.position, Quaternion.identity);
                print(_barrel.forward.magnitude * _force);
				tempBullet.Rigidbody.AddForce(_barrel.forward * _force);
				if (_isRaycast)
				{
					RaycastHit hit;
					Ray ray = _main.ScreenPointToRay(_centr);
					if (Physics.Raycast(ray, out hit))
					{
						var tempObj = hit.collider.gameObject.GetComponent<ISetDamage>();
						if (tempObj != null)
						{
							tempObj.SetHp(new BulletCollisonInfo
							{
								Damage = _damage,
								Direction = transform.forward

							});
						}
					}
				}
				_isFire = false;
				_timer.Start(_rechargeTime);
			}
		}
	}
}