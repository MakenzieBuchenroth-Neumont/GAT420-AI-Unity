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
        int ran = Random.Range(1, 6);
        if (ran == 1 || ran == 2 || ran == 3) {
            var enemies = agent.enemyPerception.getGameObjects();
            if (enemies.Length > 0) {
                agent.stateMachine.setState(nameof(AI_ChaseState));
            }
            var allies = agent.allyPerception.getGameObjects();
            if (allies.Length > 0) {
                agent.stateMachine.setState(nameof(AI_WaveState));
            }
        }
        else if (ran == 4) {
            agent.stateMachine.setState(nameof(AI_DanceState));
        }
        else if (ran == 5) {
            agent.stateMachine.setState(nameof(AI_SitUpState));
        }
	}

    public override void OnExit() {
        Debug.Log("Idle exit");
    }
}