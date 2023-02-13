using UnityEngine;

namespace GeekBrains
{
	public class SelectWeapons : MonoBehaviour
	{
		[SerializeField] private GameObject _weapon;

		public void SetColor(float value)
		{
			var color = _weapon.GetComponent<Renderer>().sharedMaterial.color;
			color.r = value;
			_weapon.GetComponent<Renderer>().sharedMaterial.color = color;
		}
	}
}