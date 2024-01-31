using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateMachine {
    private Dictionary<string, AI_State> states = new Dictionary<string, AI_State>();
    public AI_State currentState { get; private set; } = null;

    public void Update() {
        currentState?.OnUpdate();
    }

    public void addState(string name, AI_State state) {
        Debug.Assert(!states.ContainsKey(name), "State machine already contains state " + name);
        states[name] = state;
    }

    public void setState(string name) {
        Debug.Assert(states.ContainsKey(name), "State machine does not contain state " + name);
        var state = states[name];

        // don't re-enter state
        if (state == currentState) return;

        // exit current state
        currentState?.OnExit();
        // set new state
        currentState = state;
        // enter new state
        currentState?.OnEnter();
    }
}
