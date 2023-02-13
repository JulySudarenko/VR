using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GeekBrains
{
	public class Screenshot : MonoBehaviour
	{
		private bool _isProcessed;
		private string _path;
		private int _layers = 5;
		private int _resolution = 5;
		private Camera _camera;

		[SerializeField] private Button _firstMethodButton;
		[SerializeField] private Button _secondMethodButton;

		private void Start()
		{
			_camera = Camera.main;
			_path = Application.dataPath;
			if (_firstMethodButton != null) _firstMethodButton.onClick.AddListener(FirstMethod);
			if (_secondMethodButton != null) _secondMethodButton.onClick.AddListener(SecondMethod);
		}

		private void FirstMethod()
		{
			var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
			ScreenCapture.CaptureScreenshot(_path + filename, _resolution);
		}

		private void SecondMethod()
		{
			StartCoroutine(SaveScreenshot());
		}

		private IEnumerator SaveScreenshot()
		{
			_isProcessed = true;
			_secondMethodButton.interactable = false;
			yield return new WaitForEndOfFrame();
			_camera.cullingMask = ~(1 << _layers);
			var sw = Screen.width;
			var sh = Screen.height;
			var sc = new Texture2D(sw, sh, TextureFormat.RGB24, false);

			sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);

			var bytes = sc.EncodeToPNG();

			var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);

			System.IO.File.WriteAllBytes(_path + filename, bytes);

			yield return new WaitForSeconds(2.3f);

			_camera.cullingMask |= 1 << _layers;
			_isProcessed = false;
			_secondMethodButton.interactable = true;
			StopCoroutine(SaveScreenshot());
		}
	}
}