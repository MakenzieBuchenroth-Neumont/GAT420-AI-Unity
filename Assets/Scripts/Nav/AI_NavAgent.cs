using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI_NavPath))]
public class AI_NavAgent : AI_Agent {
	[SerializeField] private AI_NavPath path;

	void Update() {
		if (path.HasPath()) {
			Debug.DrawLine(transform.position, path.destination);
			movement.moveTowards(path.destination);
		}
	}
}
