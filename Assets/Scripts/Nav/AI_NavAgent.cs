using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AI_NavPath))]
public class AI_NavAgent : AI_Agent {
	[SerializeField] private AI_NavPath path;
    [SerializeField] AI_NavNode startNode;

	private void Start() {
		startNode ??= GetNearestAINavNode();
        path.destination = startNode.transform.position;
	}

	void Update() {
		if (path.HasTarget()) {
			movement.moveTowards(path.destination);
		}
        else {
            AI_NavNode destinationNode = AI_NavNode.GetRandomAINavNode();
            path.destination = destinationNode.transform.position;
        }
	}

    #region AI_NAVNODE

    public AI_NavNode GetNearestAINavNode() {
        var nodes = AI_NavNode.GetAINavNodes().ToList();
        SortByDistance(nodes);

        return (nodes.Count == 0) ? null : nodes[0];
    }

    public AI_NavNode GetNearestAINavNode(Vector3 position) {
        var nodes = AI_NavNode.GetAINavNodes();
        AI_NavNode nearest = null;
        float nearestDistance = float.MaxValue;
        foreach (var node in nodes) {
            float distance = (position - node.transform.position).sqrMagnitude;
            if (distance < nearestDistance) {
                nearest = node;
                nearestDistance = distance;
            }
        }

        return nearest;
    }

    public void SortByDistance(List<AI_NavNode> nodes) {
        nodes.Sort(CompareDistance);
    }

    public int CompareDistance(AI_NavNode a, AI_NavNode b) {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
    #endregion
}
