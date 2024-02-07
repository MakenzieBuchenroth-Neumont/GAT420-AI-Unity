using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PatrolState : AI_State {
    Vector3 destination;
    public AI_PatrolState(AI_StateAgent agent) : base(agent) {
		AI_StateTransition transition = new AI_StateTransition(nameof(AI_IdleState));
		transition.AddCondition(new FloatCondition(agent.destinationDistance, Condition.Predicate.LESS, 1));
		transitions.Add(transition);

		transition = new AI_StateTransition(nameof(AI_ChaseState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transitions.Add(transition);
	}

    public override void OnEnter() {
        agent.movement.resume();
        agent.movement.Velocity.Equals(agent.movement.maxSpeed);
        var navNode = AI_NavNode.GetRandomAINavNode();
        destination = navNode.transform.position;
    }

    public override void OnUpdate() {
        agent.movement.moveTowards(destination);
    }

    public override void OnExit() {
    }
}