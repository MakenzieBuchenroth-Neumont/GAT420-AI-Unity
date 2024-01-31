using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackState : AI_State {
    public AI_AttackState(AI_StateAgent agent) : base(agent) {
    }

    public override void OnEnter() {
        Debug.Log("Attack enter");
    }

    public override void OnExit() {
    }

    public override void OnUpdate() {
    }
}