using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateAgent : AI_Agent {
    [SerializeField] AI_Perception enemyPerception;
    AI_StateMachine stateMachine = new AI_StateMachine();

    private void Start() {
        // add states to state machine
        stateMachine.addState(nameof(AI_IdleState), new AI_IdleState(this));
        stateMachine.addState(nameof(AI_PatrolState), new AI_PatrolState(this));
        stateMachine.addState(nameof(AI_AttackState), new AI_AttackState(this));
        stateMachine.addState(nameof(AI_DeathState), new AI_DeathState(this));

        stateMachine.setState(nameof(AI_IdleState));
    }

    private void Update() {
        var enemies = enemyPerception.getGameObjects();
        if (enemies.Length > 0) {
            stateMachine.setState(nameof(AI_AttackState));
        }
        else {
            stateMachine.setState(nameof(AI_IdleState));
        }
        stateMachine.Update();
    }
}
