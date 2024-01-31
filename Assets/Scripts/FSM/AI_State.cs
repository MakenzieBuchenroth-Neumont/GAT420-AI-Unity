using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_State {
    private AI_StateAgent agent;
    public AI_State(AI_StateAgent agent) {
        this.agent = agent;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
}
