using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateAgent : AI_Agent {
    public AI_Perception enemyPerception;
    public AI_Perception allyPerception;
    public Animator animator;

	// parameters
	public ValueRef<float> health = new ValueRef<float>(); // -> memory
	public ValueRef<float> timer = new ValueRef<float>(); // -> memory
	public ValueRef<float> destinationDistance = new ValueRef<float>(); // -> memory

	public ValueRef<bool> enemySeen = new ValueRef<bool>();
	public ValueRef<float> enemyDistance = new ValueRef<float>();
	public ValueRef<float> enemyHealth = new ValueRef<float>();

	public ValueRef<bool> allySeen = new ValueRef<bool>();
	public ValueRef<float> allyDistance = new ValueRef<float>();
	public ValueRef<float> allyHealth = new ValueRef<float>();

    public AI_StateMachine stateMachine = new AI_StateMachine();
	public AI_StateAgent enemy { get; private set; }

    private void Start() {
		health.value = 100;

        // add states to state machine
        stateMachine.addState(nameof(AI_IdleState), new AI_IdleState(this));
        stateMachine.addState(nameof(AI_PatrolState), new AI_PatrolState(this));
        stateMachine.addState(nameof(AI_AttackState), new AI_AttackState(this));
        stateMachine.addState(nameof(AI_DeathState), new AI_DeathState(this));
        stateMachine.addState(nameof(AI_ChaseState), new AI_ChaseState(this));
        stateMachine.addState(nameof(AI_WaveState), new AI_WaveState(this));
        stateMachine.addState(nameof(AI_DanceState), new AI_DanceState(this));
        stateMachine.addState(nameof(AI_SitUpState), new AI_SitUpState(this));
        stateMachine.addState(nameof(AI_HitState), new AI_HitState(this));

        stateMachine.setState(nameof(AI_IdleState));
    }

    private void Update() {
		// update parameters
		timer.value -= Time.deltaTime;
		destinationDistance.value = Vector3.Distance(transform.position, movement.Destination);

		var enemies = enemyPerception.getGameObjects();
		enemySeen.value = (enemies.Length > 0);
		if (enemySeen) {
			enemy = enemies[0].TryGetComponent(out AI_StateAgent stateAgent) ? stateAgent : null;
			enemyDistance.value = Vector3.Distance(transform.position, enemy.transform.position);
			enemyHealth.value = enemy.health;
		}

		// from any state ( health -> death)
        if (health <= 0) {
            stateMachine.setState(nameof(AI_DeathState));
        }

        animator?.SetFloat("Speed", movement.Velocity.magnitude);

		// check for transition
		foreach (var transition in stateMachine.currentState.transitions) {
			if (transition.ToTransition()) {
				stateMachine.setState(transition.nextState);
				break;
			}
		}

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

	private void Attack() {
		// check for collision with surroundings
		var colliders = Physics.OverlapSphere(transform.position, 1);
		foreach (var collider in colliders) {
			// don't hit self or objects with the same tag
			if (collider.gameObject == gameObject || collider.gameObject.CompareTag(gameObject.tag)) continue;

			// check if collider object is a state agent, reduce health
			if (collider.gameObject.TryGetComponent<AI_StateAgent>(out var stateAgent)) {
				stateAgent.applyDamage(Random.Range(20, 50));
			}
		}
	}

	public void applyDamage(float damage) {
		health.value -= damage;
		if (health.value > 0) stateMachine.setState(nameof(AI_HitState));
	}
}
