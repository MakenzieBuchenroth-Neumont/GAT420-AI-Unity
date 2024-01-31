using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_NavMeshMovement : AI_Movement {
	[SerializeField] NavMeshAgent navMeshAgent;

	public override Vector3 Velocity {
		get => navMeshAgent.velocity;
		set => navMeshAgent.velocity = value;
	}

	public override Vector3 Destination {
		get => navMeshAgent.destination;
		set => navMeshAgent.destination = value;
	}

	void LateUpdate() {
		navMeshAgent.speed = maxSpeed;
		navMeshAgent.acceleration = maxForce;
		navMeshAgent.angularSpeed = turnRate;
	}

	public override void applyForce(Vector3 force) {
		//
	}

	public override void moveTowards(Vector3 target) {
		navMeshAgent.SetDestination(target);
	}

	public override void resume() {
		navMeshAgent.isStopped = false;
	}

	public override void stop() {
		navMeshAgent.isStopped = true;
	}
}
