using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeekBrains
{
	
	public class FlamesSO : ScriptableObject
	{
		[SerializeField] private Flames Flames;
		[SerializeField] private float TimeLife;
		[SerializeField] private int Count;
		public float Damage;
		private Queue<Flames> _flameses = new Queue<Flames>();

		public void Start()
		{
			for (var i = 1; i <= Count; i++)
			{
				var temp = Instantiate(Flames, Vector3.one, Quaternion.identity);
				temp.SetActive(false);
				temp.TimeLife = TimeLife;
				temp.Message += DisableFlames;
				_flameses.Enqueue(temp);
			}
		}

		public void DisableFlames(object obj, DisableFlamesEventArgs disableFlamesEventArgs)
		{
			var flames = obj as Flames;
			if (flames == null) return;
			flames.SetActive(false);
			flames.transform.position = Vector3.one;
			flames.transform.rotation = Quaternion.identity;
			flames.transform.parent = null;
			_flameses.Enqueue(flames);
		}

		public void EnableFlames(Vector3 pos, Quaternion rot, Transform parent)
		{
			if (_flameses.Count <= 0)return;
			var temp = _flameses.Dequeue();
			temp.transform.position = pos;
			temp.transform.rotation = rot;
			temp.transform.parent = parent;
			temp.SetActive(true);
		}
	}
	public class DisableFlamesEventArgs : EventArgs
	{
		public DisableFlamesEventArgs()
		{
		}
	}
}