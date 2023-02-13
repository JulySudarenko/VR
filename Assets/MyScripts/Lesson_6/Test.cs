using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public Button Button;
	private Interface Interface;
	void Start ()
	{
		Button.onClick.AddListener(Click);
		Interface = GetComponent<Interface>();
	}
	
	void Click ()
	{
		//StartCoroutine(Interface.LoadSceneAsync(Main.Instance.Scenes.Game));
		
	}
}
