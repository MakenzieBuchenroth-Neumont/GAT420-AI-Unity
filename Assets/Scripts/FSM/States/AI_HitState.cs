using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HitState : AI_State {
	float timer = 0;
	public AI_HitState(AI_StateAgent agent) : base(agent) {}

	public override void OnEnter() {
		agent.animator?.SetTrigger("Hit");
	}

	public override void OnUpdate() {
	}

	public override void OnExit() {
	}
}