using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackState : AI_State {
    public AI_AttackState(AI_StateAgent agent) : base(agent) {
		AI_StateTransition transition = new AI_StateTransition(nameof(AI_ChaseState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);
	}

    public override void OnEnter() {
        agent.movement.stop();
        agent.movement.Velocity = Vector3.zero;
		agent.animator?.SetTrigger("Attack");
        agent.timer.value = 2;
	}

    public override void OnUpdate() {
    }

    public override void OnExit() {
    }
}