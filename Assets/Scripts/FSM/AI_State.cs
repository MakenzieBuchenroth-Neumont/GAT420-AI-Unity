using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_State {
    protected AI_StateAgent agent;
    public AI_State(AI_StateAgent agent) {
        this.agent = agent;
    }

    public List<AI_StateTransition> transitions { get; set; } = new List<AI_StateTransition>();

    public string name { get { return GetType().Name; } }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
