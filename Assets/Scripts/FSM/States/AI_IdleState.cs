using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AI_IdleState : AI_State {
    float initialSpeed;
    public AI_IdleState(AI_StateAgent agent) : base(agent) {
		AI_StateTransition transition = new AI_StateTransition(nameof(AI_PatrolState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transitions.Add(transition);

        transition = new AI_StateTransition(nameof(AI_ChaseState));
        transition.AddCondition(new BoolCondition(agent.enemySeen));
        transitions.Add(transition);
    }

    public override void OnEnter() {
		initialSpeed = agent.movement.maxSpeed;
		agent.movement.stop();
        agent.movement.Velocity = Vector3.zero;

        agent.timer.value = Random.Range(1, 2);
    }

    public override void OnUpdate() {
        //if (transition.ToTransition()) agent.stateMachine.setState(transition.nextState);


        //int ran = Random.Range(1, 6);
        //if (ran == 1 || ran == 2 || ran == 3) {
        //    
        //    var allies = agent.allyPerception.getGameObjects();
        //    if (allies.Length > 0) {
        //        agent.stateMachine.setState(nameof(AI_WaveState));
        //    }
        //}
        //else if (ran == 4) {
        //    agent.stateMachine.setState(nameof(AI_DanceState));
        //}
        //else if (ran == 5) {
        //    agent.stateMachine.setState(nameof(AI_SitUpState));
        //}
    }

    public override void OnExit() {
        Debug.Log("Idle exit");
		agent.movement.maxSpeed = initialSpeed;
	}
}