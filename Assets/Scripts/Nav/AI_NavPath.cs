using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AI_NavAgent))]
public class AI_NavPath : MonoBehaviour {
	public enum ePathType {
		Waypoint,
		Dijkstra,
		AStar
	}
	[SerializeField] AI_NavAgent agent;
	[SerializeField] ePathType pathType;

	List<AI_NavNode> path = new List<AI_NavNode>();

	public AI_NavNode targetNode { get; set; } = null;
	public Vector3 destination {
		get {
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero;
		}
		set {
			if (pathType == ePathType.Waypoint) {
				targetNode = agent.GetNearestAINavNode(value);
			}
			else if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar) {
				AI_NavNode startNode = agent.GetNearestAINavNode();
				AI_NavNode endNode = agent.GetNearestAINavNode(value);
				generatePath(startNode, endNode);
				targetNode = startNode;
			}
		}
	}

	public bool HasTarget() {
		return targetNode != null;
	}

	public AI_NavNode GetNextAINavNode(AI_NavNode node) {
		if (pathType == ePathType.Waypoint) return node.GetRandomNeighbor();
		if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar) return getNextPathAINavNode(node);
		
		return null;
	}

	private void generatePath(AI_NavNode startNode, AI_NavNode endNode) {
		AI_NavNode.ResetNodes();
		if (pathType == ePathType.Dijkstra) AI_NavDijkstra.generate(startNode, endNode, ref path);
		if (pathType == ePathType.AStar) AI_NavAStar.generate(startNode, endNode, ref path);
	}

	private AI_NavNode getNextPathAINavNode(AI_NavNode node) {
		if (path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);
		// if not found or past the end return null
		if (index + 1 == path.Count || index == -1) return null;

		//get next node in path
		AI_NavNode nextNode = path[index + 1];

		return nextNode;
	}

	private void OnDrawGizmosSelected() {
		if (path.Count == 0) return;

		var pathArray = path.ToArray();

		for (int i = 1; i < path.Count - 1; i++) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(pathArray[i].transform.position + Vector3.up, 1);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawSphere(pathArray[0].transform.position + Vector3.up, 1);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(pathArray[pathArray.Length - 1].transform.position + Vector3.up, 1);
	}
}
