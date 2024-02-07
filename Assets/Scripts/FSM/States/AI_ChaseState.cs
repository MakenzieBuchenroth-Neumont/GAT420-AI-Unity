using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ChaseState : AI_State {
	float initialSpeed;
	public AI_ChaseState(AI_StateAgent agent) : base(agent) {
		AI_StateTransition transition = new AI_StateTransition(nameof(AI_AttackState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new FloatCondition(agent.enemyDistance, Condition.Predicate.LESS, 1));
		transitions.Add(transition);

		transition = new AI_StateTransition(nameof(AI_IdleState));
		transition.AddCondition(new BoolCondition(agent.enemySeen, false));
		transitions.Add(transition);
	}

	public override void OnEnter() {
		initialSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed *= 2;
	}

	public override void OnUpdate() {
		if (agent.enemySeen) agent.movement.Destination = agent.enemy.transform.position;
	}

	public override void OnExit() {
		agent.movement.maxSpeed = initialSpeed;
	}
}
