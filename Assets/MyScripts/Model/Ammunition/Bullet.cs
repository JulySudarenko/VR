
using GeekBrains.Helpers;
using UnityEngine;

namespace GeekBrains
{
	public class Bullet: Ammunition
	{
		[SerializeField] private BulletProjector _bulletProjector;
		private void OnCollisionEnter(Collision collision)
		{
			SetDamage(collision.collider.GetComponent<ISetDamage>());

			var contact = collision.contacts[0];
			CreateDecal(contact, collision.transform);
			Destroy(InstanceObject);
		}

		private void SetDamage(ISetDamage obj)
		{
			if (obj == null) return;
			obj.SetHp(new BulletCollisonInfo
            {
                Damage = CurrentDamage,
                Direction = Rigidbody.velocity
            });
		}

		private void CreateDecal(ContactPoint contact, Transform objCollision)
		{
			var projectorRotation = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
			if (_bulletProjector != null)
			{
				var obj = Instantiate(_bulletProjector, contact.point + contact.normal * 0.25f, projectorRotation, objCollision);
				obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, Random.Range(0, 360));
			}
		}
	}
}