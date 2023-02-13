using UnityEngine;

namespace GeekBrains
{
	//[CreateAssetMenu(fileName = "Data", menuName = "CreateScriptableObject/Weapon", order = 1)]
	public class WeaponScriptableObject : ScriptableObject
	{
		public string Name = "New MyScriptableObject";
		public float Power;
		public Color Color = Color.white;
		public Sprite Ico;
	}
}