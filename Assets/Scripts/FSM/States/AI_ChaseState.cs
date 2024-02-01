using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ChaseState : AI_State {
	float initialSpeed;
	public AI_ChaseState(AI_StateAgent agent) : base(agent) {

	}

	public override void OnEnter() {
		initialSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed *= 2;
	}

	public override void OnUpdate() {
		var enemies = agent.enemyPerception.getGameObjects();
		if (enemies.Length > 0 ) {
			var enemy = enemies[0];
			if (Vector3.Distance(agent.transform.position, enemy.transform.position) < 1.25f) {
				agent.stateMachine.setState(nameof(AI_AttackState));
			}
		}
		else {
			agent.stateMachine.setState(nameof(AI_IdleState));
		}
	}

	public override void OnExit() {
		agent.movement.maxSpeed = initialSpeed;
	}
}
