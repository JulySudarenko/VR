using System;
using System.Linq;
using GeekBrains.Helpers;
using UnityEngine;

namespace GeekBrains
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый класс всех объектов на сцене
    /// </summary>
    public abstract class BaseObjectScene : MonoBehaviour
    {
        private int _layer;
        private Color _color;
	    private Vector3 _center;
	    private Bounds _bound;
	    private bool _isVisible;
		[HideInInspector] public Transform Transform;
	    [HideInInspector] public GameObject InstanceObject;
		[HideInInspector] private Rigidbody _rigidbody;

		#region UnityFunction

		protected virtual void Awake()
        {
	        InstanceObject = gameObject;
	        Transform = transform;
			Rigidbody = InstanceObject.GetComponent<Rigidbody>();
        }
        #endregion

        #region Property

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name
        {
            get { return InstanceObject.name; }
            set
            {
	            InstanceObject.name = value;
            }
        }

        /// <summary>
        /// Слой объекта
        /// </summary>
        public int Layers
        {
            get { return _layer; }

            set
            {
                _layer = value;
                AskLayer(Transform, value);
            }
        }

        /// <summary>
        /// Цвет материала объекта
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                AskColor(Transform, _color);
            }
        }

        public Vector3 Center
        {
            get
            {
                var rends = InstanceObject.GetComponentsInChildren<Renderer>();
                var bounds = rends[0].bounds;
                bounds = rends.Aggregate(bounds, (current, rend) => current.GrowBounds(current));
                _center = bounds.center;
                return _center;
            }
        }

        public Bounds Bound
        {
            get
            {
                var rends = InstanceObject.GetComponentsInChildren<Renderer>();
                var bounds = rends[0].bounds;
                bounds = rends.Aggregate(bounds, (current, rend) => current.GrowBounds(current));
                _bound = bounds;
                return _bound;
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
	            var tempRenderer = InstanceObject.GetComponent<Renderer>();
				if (tempRenderer)
					tempRenderer.enabled = _isVisible;
                if (Transform.childCount <= 0) return;
                foreach (Transform d in Transform)
                {
	                tempRenderer = d.gameObject.GetComponent<Renderer>();
					if (tempRenderer)
						tempRenderer.enabled = _isVisible;
                }
            }
        }

		public Rigidbody Rigidbody
		{
			get
			{
				return _rigidbody;
			}

			private set
			{
				 _rigidbody = value;
			}
		}

		#endregion

		#region PrivateFunction
		/// <summary>
		/// Выставляет слой себе и всем вложенным объектам в независимости от уровня вложенности
		/// </summary>
		/// <param name="obj">Объект</param>
		/// <param name="lvl">Слой</param>
		private void AskLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskLayer(d, lvl);
            }
        }

        private void AskColor(Transform obj, Color color)
        {
            foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
            {
                curMaterial.color = color;
            }
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskColor(d, color);
            }
        }
        #endregion

        public bool IsRigitBody()
        {
            return Rigidbody;
        }

        public bool RayCheck(Vector3 direction, int layer)
        {
            var tempLayer = Layers;
            Layers = 2;
            Ray ray = new Ray(Center, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Bound.extents.magnitude + 0.05f, layer))
            {
                return hit.collider.gameObject;
            }
            Layers = tempLayer;
            return false;
        }

        public string RayCheck(Vector3 direction, int layer, float lenght = 0.05f)
        {
            //var tempLayer = Layers;
            //Layers = 2;
            Ray ray = new Ray(Center, direction);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Bound.extents.magnitude + lenght, layer)) return String.Empty;
            return hit.collider.gameObject ? hit.collider.gameObject.name : String.Empty;
            //Layers = tempLayer;
        }

        /// <summary>
        /// Выключает физику у объекта и его детей
        /// </summary>
        public void DisableRigidBody()
        {
            if (!IsRigitBody()) return;

            Rigidbody[] rigidbodies = InstanceObject.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
		}

	    /// <summary>
	    /// Включает физику у объекта и его детей
	    /// </summary>
	    public void EnableRigidBody(float force)
	    {
		    EnableRigidBody();
			//Rigidbody.isKinematic = false;
		    Rigidbody.AddForce(transform.forward * force);
		}

	    /// <summary>
	    /// Включает физику у объекта и его детей
	    /// </summary>
	    public void EnableRigidBody()
	    {
		    if (!IsRigitBody()) return;
		    Rigidbody[] rigidbodies = InstanceObject.GetComponentsInChildren<Rigidbody>();
		    foreach (var rb in rigidbodies)
		    {
			    rb.isKinematic = false;
		    }
	    }

		/// <summary>
		/// Замораживает или размораживает физическую трансформацию объекта
		/// </summary>
		/// <param name="rigidbodyConstraints">Трансформацию которую нужно заморозить</param>
		public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
        {
            Rigidbody[] rigidbodies = InstanceObject.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.constraints = rigidbodyConstraints;
            }
		}

	    public void SetActive(bool value)
	    {
		    IsVisible = value;

		    var tempCollider = GetComponent<Collider>();
		    if (tempCollider)
		    {
			    tempCollider.enabled = value;
		    }
	    }
	}
}
