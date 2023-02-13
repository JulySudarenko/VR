using UnityEngine;
using UnityEngine.EventSystems;

namespace GeekBrains
{
	public class InteractiveObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			Debug.Log("OnPointerDown");
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Debug.Log("OnPointerUp");
		}
	}
}