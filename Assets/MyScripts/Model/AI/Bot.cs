using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

namespace GeekBrains
{
    [RequireComponent(typeof(NavMeshAgent))]
	public class Bot : BaseObjectScene, ISetDamage
	{
		[HideInInspector] public NavMeshAgent Agent;
		[HideInInspector] public Transform Target;

		protected bool _isDead;
		private float _hp = 100;
		public float Hp
		{
			get
			{
				return _hp;
			}

			set
			{
				_hp = value;
			}
		}

		[SerializeField] protected Vision _vision;
        protected Patrol _patrol;
        protected bool _isTarget;
        protected float _curTime;
        protected const float TIMEWAINT = 3;
		
		[SerializeField] protected Weapons _weapons;
		[SerializeField] protected HeadBot _headBot;	
        [SerializeField] protected float fireDistance = 5f; // расстояние с которого будет пиу пиу

		protected override void Awake()
		{
			base.Awake();
			Agent = GetComponent<NavMeshAgent>();
		}

        protected virtual void Start()
		{
			_patrol= new Patrol();
			Target = FindObjectOfType<FirstPersonController>().transform;		

			if (_headBot != null) _headBot.HeadShot = SetHp;
		}

        protected void Update()
		{
			if (_isDead)return;
			if (!_isTarget)
			{
				if (!Agent.hasPath)
				{
					_curTime += Time.deltaTime;
					if (_curTime >= TIMEWAINT)
					{
						_curTime = 0;
						_patrol.GenericPoint(Agent);
					}
				}
				if (_vision.VisionMaht(Transform, Target))
				{
					_isTarget = true;				
				}
			}
			else
			{
				Agent.SetDestination(Target.position);
				Agent.stoppingDistance = fireDistance; // достижение минимального расстояния

				if (_vision.VisionMaht(Transform, Target))
				{
					if (_weapons != null) _weapons.Fire();
				}
				else if(!_vision.Dist(Transform, Target)) 
				{
					_isTarget = false;
					_patrol.GenericPoint(Agent);
				}
			}
		}

		public void SetHp(BulletCollisonInfo info)
		{
			if (_isDead) return;
			if (Hp > 0)
			{
				_isTarget = true;
				Hp -= info.Damage;
			}

			if (Hp <= 0)
			{
				_isDead = true;
				Agent.enabled = false;
				foreach (Transform child in GetComponentsInChildren<Transform>())
				{
					child.parent = null;
					
					if(!child.gameObject.GetComponent<Rigidbody>())
					{
						child.gameObject.AddComponent<Rigidbody>().AddForce(info.Direction.normalized * Random.Range(100,1000));
					}
					Destroy(child.gameObject, 10);
				}
			}
		}        
	}
}