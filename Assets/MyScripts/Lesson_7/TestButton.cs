using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scrips.Lesson_7
{
	public class TestButton : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private Text _text;
		private int _count;

		private int Text
		{
			set { _text.text = value.ToString(); }
		}
		private void OnValidate()
		{
			_button = GetComponent<Button>();
			_text = transform.GetComponentInChildren<Text>();

			EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry{eventID = EventTriggerType.PointerEnter};
			entry.callback.AddListener(delegate(BaseEventData arg0) { Call(); });

			trigger.triggers.Add(entry);
		}

		private void Call()
		{
			Text = ++_count;
		}
	}
}