using UnityEngine;

namespace GeekBrains
{
	public class CreateObject : MonoBehaviour
	{
		[SerializeField] private int _countObj = 5;
		[SerializeField] private int _offset = 1;
		[SerializeField] private GameObject _prefab;
		[SerializeField] private Color _color;
		private Transform _root;

		private void Start()
		{
			CreateObj();
		}

		public void CreateObj()
		{
			if (_prefab == null) return;
			_root = new GameObject("root").transform;
			for (var i = 1; i <= _countObj; i++)
			{ Instantiate(_prefab, new Vector3(0, i + _offset, 0), Quaternion.identity, _root);
			}
		}
	}
}