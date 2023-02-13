using System.Collections;
using UnityEngine;

namespace GeekBrains
{
	public class Mina : MonoBehaviour
	{
		public float Radius;
		public float Force;
		[SerializeField] private GameObject _light;

		private float _timeLight;

		private void Start()
		{
			_light.GetComponent<Light>().color = Color.red;
			StartCoroutine(Light());
			_timeLight = 1;
			Explosion();
		}


		private void OnTriggerExit(Collider other)
		{
			Explosion();
		}

		private void Explosion()
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
			foreach (Collider obj in hitColliders)
			{
				var tempRigidbody = obj.GetComponent<Rigidbody>();
				if (!tempRigidbody) continue;
				tempRigidbody.useGravity = true;
				tempRigidbody.isKinematic = false;
				tempRigidbody.AddExplosionForce(Force, transform.position, Radius, 0.0F);

				//var tempObj = obj.GetComponent<ISetDamage>();
				//if (tempObj == null) continue;
				//obj.GetComponent<ISetDamage>().ApplyDamage(1000);
			}
		}

		//private void OnTriggerExit(Collider other)
		//{
		//    _timeLight = 1f;
		//}

		private void OnTriggerEnter(Collider other)
		{
			_timeLight = 0.5f;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, Radius);
		}

		IEnumerator Light()
		{
			while (true)
			{
				_light.SetActive(!_light.activeSelf);
				yield return new WaitForSeconds(_timeLight);
			}
		}
	}
}