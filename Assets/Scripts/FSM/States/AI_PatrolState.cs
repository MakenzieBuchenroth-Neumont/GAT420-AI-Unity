using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PatrolState : AI_State {
    Vector3 destination;
    public AI_PatrolState(AI_StateAgent agent) : base(agent) {
    }

    public override void OnEnter() {
        var navNode = AI_NavNode.GetRandomAINavNode();
        destination = navNode.transform.position;
    }

    public override void OnUpdate() {
        // move towards random node, go to idle if reached
        agent.movement.moveTowards(destination);
        if (Vector3.Distance(agent.transform.position, destination) < 1) {
            agent.stateMachine.setState(nameof(AI_IdleState));
        }

        var enemies = agent.enemyPerception.getGameObjects();
        if (enemies.Length > 0) {
            agent.stateMachine.setState(nameof(AI_ChaseState));
        }
    }

    public override void OnExit() {
    }
}