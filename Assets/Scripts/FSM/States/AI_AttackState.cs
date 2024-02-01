using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackState : AI_State {
    float timer = 0;
    public AI_AttackState(AI_StateAgent agent) : base(agent) {
    }

    public override void OnEnter() {
		agent.animator?.SetTrigger("Attack");
        timer = Time.time + 2;
	}

    public override void OnUpdate() {
        if (Time.time >= timer) {
            agent.stateMachine.setState(nameof(AI_IdleState));
        }
    }

    public override void OnExit() {
    }
}