using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SitUpState : AI_State
{
	float initialSpeed;
	float timer = 0;
	public AI_SitUpState(AI_StateAgent agent) : base(agent){ }

	public override void OnEnter() {
		initialSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed = 0;
		agent.animator?.SetTrigger("Situp");
		timer = Time.time + 3;
	}

	public override void OnUpdate() {
		if (Time.time >= timer) {
			agent.stateMachine.setState(nameof(AI_IdleState));
		}
	}

	public override void OnExit() {
		agent.movement.maxSpeed = initialSpeed;
	}
}