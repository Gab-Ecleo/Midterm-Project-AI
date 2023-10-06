using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
	#region Variables

	public Transform _waypoint;
	public Transform _targetWaypoint;

	public float _movementRad;

	private NavMeshAgent _agent;
	private bool _isFollowing;

	#endregion

	#region Unity_Methods

	IEnumerator Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_isFollowing = false;

		while (true)
		{
			yield return StartCoroutine(AIMovement(_waypoint));
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			_isFollowing = true;
	}

	#endregion

	#region Methods

	IEnumerator AIMovement(Transform _waypoint)
	{
		if (_isFollowing)
		{
			_agent.SetDestination(_waypoint.position);
			yield return null;
		}
		else
		{
			Vector3 newPos = AIMovementRadius(transform.position, _movementRad, -1);
			_agent.SetDestination(newPos);
			yield return new WaitForSeconds(2);
		}
	}

	private Vector3 AIMovementRadius(Vector3 _origin, float _dist, int _layermask)
	{
		Vector3 _randDir = UnityEngine.Random.insideUnitSphere * _dist;

		_randDir += _origin;

		NavMeshHit _navHit;

		NavMesh.SamplePosition(_randDir, out _navHit, _dist, _layermask);

		return _navHit.position;
	}

	#endregion
}
