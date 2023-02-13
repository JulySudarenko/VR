using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyNamespace
{
	public class Test : MonoBehaviour, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			Debug.Log(gameObject.name);
		}

		private void OnGUI()
		{
			var e = Event.current;
			if (e.isKey)
			{
				Debug.Log(e.keyCode);
			}
		}
	}
}

