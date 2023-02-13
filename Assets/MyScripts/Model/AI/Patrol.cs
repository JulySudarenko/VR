using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace GeekBrains
{
	public class Patrol
	{
		private List<Vector3> _listPoint = new List<Vector3>();
		private int _indexCurPoint;
		private NavMeshPath _path;
		private int _maxDist = 150;
		private int _minDist = 30;

		public Patrol()
		{
		//	var temp = GameObject.FindGameObjectsWithTag("WayPoint");

		//	_listPoint = temp.Select(o => o.transform.position).ToList();
		}

		public void GenericPoint(NavMeshAgent agent, bool isRandom = true)
		{
			Vector3 result;
			if (isRandom)
			{
				int randomDist = Random.Range(_minDist, _maxDist);
				Vector3 randomPoint = Random.insideUnitCircle * randomDist;
				NavMeshHit hit;
				NavMesh.SamplePosition(agent.transform.position + randomPoint, out hit, randomDist, NavMesh.AllAreas);
				result = hit.position;
			} else
			{
				if (_indexCurPoint < _listPoint.Count - 1)
				{
					_indexCurPoint++;
				}
				else
				{
					_indexCurPoint = 0;
				}
				result = _listPoint[_indexCurPoint];
			}
			agent.SetDestination(result);
			agent.stoppingDistance = 0;
		}
	}
}