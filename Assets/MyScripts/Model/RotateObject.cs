using System.Collections;
using UnityEngine;

namespace GeekBrains
{
	public class RotateObject : MonoBehaviour
	{
		[SerializeField] private float _speed = 50;

		private void Start()
		{
			StartCoroutine(LoadSceneAsync());
		}

		private IEnumerator LoadSceneAsync()
		{
			while (true)
			{
				transform.RotateAround(transform.position, transform.up, _speed * Time.deltaTime);

				yield return new WaitForSeconds(0.01f);
			}
		}
	}
}