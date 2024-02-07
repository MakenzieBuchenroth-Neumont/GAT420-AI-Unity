using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DeathState : AI_State {
    float timer = 0;

    public AI_DeathState(AI_StateAgent agent) : base(agent) {
    }

    public override void OnEnter() {
        agent.movement.stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Death");
        timer = Time.time + 4;
    }

    public override void OnUpdate() {
        if (Time.time > timer) {
            GameObject.Destroy(agent.gameObject);
        }
    }

    public override void OnExit() {
    }
}
