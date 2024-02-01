using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AI_IdleState : AI_State {
    float timer;
    public AI_IdleState(AI_StateAgent agent) : base(agent) {
    }

    public override void OnEnter() {
        timer = Time.time + Random.Range(1, 2);
    }

    public override void OnUpdate() {
        if (timer > 0) {
			agent.stateMachine.setState(nameof(AI_PatrolState));
		}
		var enemies = agent.enemyPerception.getGameObjects();
		if (enemies.Length > 0) {
			agent.stateMachine.setState(nameof(AI_ChaseState));
		}
	}

    public override void OnExit() {
        Debug.Log("Idle exit");
    }
}