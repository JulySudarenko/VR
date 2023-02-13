using System.Collections.Generic;
using GeekBrains.Helpers;
using UnityEngine;

namespace GeekBrains
{
	public class MovingPoints : MonoBehaviour
	{
		//Aget
		[SerializeField] private Transform _point;
		private Queue<Vector3> _points = new Queue<Vector3>();
		private Camera _camera;
		private Transform _root; public Color c1 = Color.yellow;
		public Color c2 = Color.red;
		public int lengthOfLineRenderer = 20;
		private LineRenderer lineRenderer;
		void Start()
		{
			_camera = Camera.main;
			_root = new GameObject("RootPoint").transform;
			lengthOfLineRenderer = 2;
			var temp = new GameObject("LineRenderer");
			lineRenderer = temp.AddComponent<LineRenderer>();
			lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
			lineRenderer.startColor = c1;
			lineRenderer.endColor = c2;
			lineRenderer.startWidth = 0.2f;
			lineRenderer.endWidth = 0.2f;
			lineRenderer.positionCount = lengthOfLineRenderer;
		
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown((int)MouseButton.LeftButton))
			{
				RaycastHit hit;
				if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
				{
					DrawPoint(hit.point);
				}
			}
			RaycastHit hit2;
			if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit2))
			{
				lineRenderer.SetPosition(0, hit2.point);
			}
		}

		private void DrawPoint(Vector3 point)
		{

			var temp = Instantiate(_point, point, Quaternion.identity, _root);
			_points.Enqueue(temp.position);
			lineRenderer.SetPosition(1, temp.position);
		}
	}
}