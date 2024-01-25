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

	[SerializeField] ePathType pathType;
	[SerializeField] AI_NavNode startNode;
	[SerializeField] AI_NavNode endNode;

	AI_NavAgent agent;
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
				generatePath(startNode, endNode);
			}
		}
	}

	private void Start() {
		agent = GetComponent<AI_NavAgent>();
		targetNode = (startNode != null) ? startNode : AI_NavNode.GetRandomAINavNode(); 
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
		AI_NavDijkstra.generate(startNode, endNode, ref path);
	}

	private AI_NavNode getNextPathAINavNode(AI_NavNode node) {
		if (path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);
		if (index + 1 == path.Count) return null;

		AI_NavNode nextNode = path[index + 1];

		return null;
	}
}
