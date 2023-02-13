using UnityEngine;

namespace GeekBrains
{
	public class LightUser : BaseObjectScene
	{
		private Light _light;
		private Transform _goFollow;
		private Vector3 _offset;
		[SerializeField] private float _speed = 3;
		[SerializeField] private float _range = 40;
		[SerializeField] private float _spotAngle = 40;
		[SerializeField] private float _intensity = 20;
		[SerializeField] private LightShadows _shadows = LightShadows.Soft;

		protected override void Awake()
		{
			base.Awake();
			_light = GetComponent<Light>();
			_goFollow = Camera.main.transform;
			_offset = Transform.position - _goFollow.position;
			_light.range = _range;
			_light.spotAngle = _spotAngle;
			_light.intensity = _intensity;
			_light.shadows = _shadows;
			Switch(false);
		}

		public void Switch(bool value)
		{
			_light.enabled = value;

			Transform.position = _goFollow.position + _offset;
			Transform.rotation = _goFollow.rotation;
		}

		private void Update()
		{
			if(_light && !_light.enabled)return;

			Transform.position = _goFollow.position + _offset;
			Transform.rotation = Quaternion.Slerp(Transform.rotation, _goFollow.rotation, _speed* Time.deltaTime);
		}
	}
}