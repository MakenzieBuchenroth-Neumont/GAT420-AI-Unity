using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI_NavPath : MonoBehaviour {
	[SerializeField] private AI_NavNode startNode;

	public AI_NavNode targetNode { get; set; } = null;
	public Vector3 destination  { 
		get  { 
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero; 
		} 
	}

	private void Start() {
		targetNode = (startNode != null) ? startNode : AI_NavNode.GetRandomAINavNode(); 
		
	}

	public bool HasPath() {
		return targetNode != null;
	}

	public AI_NavNode GetNextAINavNode(AI_NavNode node) {
		return node.GetRandomNeighbor();
	}

	/*
	public AI_NavNode GetNearestAINavNode()
	{
		var nodes = AI_NavNode.GetAINavNodes().ToList();
		SortAINavNodesByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

	public void SortAINavNodesByDistance(List<AI_NavNode> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(AI_NavNode a, AI_NavNode b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
	*/
}
