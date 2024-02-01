using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateAgent : AI_Agent {
    public AI_Perception enemyPerception;
    public Animator animator;
    public float health = 100;

    public AI_StateMachine stateMachine = new AI_StateMachine();

    private void Start() {
        // add states to state machine
        stateMachine.addState(nameof(AI_IdleState), new AI_IdleState(this));
        stateMachine.addState(nameof(AI_PatrolState), new AI_PatrolState(this));
        stateMachine.addState(nameof(AI_AttackState), new AI_AttackState(this));
        stateMachine.addState(nameof(AI_DeathState), new AI_DeathState(this));
        stateMachine.addState(nameof(AI_ChaseState), new AI_ChaseState(this));

        stateMachine.setState(nameof(AI_IdleState));
    }

    private void Update() {
        if (health <= 0) {
            stateMachine.setState(nameof(AI_DeathState));
        }

        animator?.SetFloat("Speed", movement.Velocity.magnitude);
        stateMachine.Update();
    }

	private void OnGUI() {
		// draw label of current state above agent
		GUI.backgroundColor = Color.black;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		Rect rect = new Rect(0, 0, 100, 20);
		// get point above agent
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		rect.x = point.x - (rect.width / 2);
		rect.y = Screen.height - point.y - rect.height - 20;
		// draw label with current state name
		GUI.Label(rect, stateMachine.currentState.name);
	}
}
