using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateTransition {
	public string nextState;

	List<Condition> conditions = new List<Condition>();

	public AI_StateTransition(string nextState, List<Condition> conditions) {
		this.nextState = nextState;
		this.conditions = conditions;
	}

	public AI_StateTransition(string nextState) {
		this.nextState = nextState;
	}

	public void AddCondition(Condition condition) {
		conditions.Add(condition);
	}

	public bool ToTransition() {
		foreach (var condition in conditions) {
			if (!condition.IsTrue()) return false;
		}

		return true;
	}
}