using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AI_NavNode : MonoBehaviour {
	[SerializeField] public List<AI_NavNode> neighbors = new List<AI_NavNode>();

	public AI_NavNode GetRandomNeighbor() {
		return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.TryGetComponent<AI_NavPath>(out AI_NavPath navPath)) {
			if (navPath.targetNode == this) {
				navPath.targetNode = GetRandomNeighbor();
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.TryGetComponent<AI_NavPath>(out AI_NavPath navPath)) {
			if (navPath.targetNode == this) {
				navPath.targetNode = GetRandomNeighbor();
			}
		}
	}


	#region HELPER_FUNCTIONS

	public static AI_NavNode[] GetAINavNodes()
	{
		return FindObjectsOfType<AI_NavNode>();
	}

	public static AI_NavNode[] GetAINavNodesWithTag(string tag)
	{
		var allNodes = GetAINavNodes();

		// add nodes with tag to nodes
		List<AI_NavNode> nodes = new List<AI_NavNode>();
		foreach (var node in allNodes)
		{
			if (node.CompareTag(tag))
			{
				nodes.Add(node);
			}
		}

		return nodes.ToArray();
	}

	public static AI_NavNode GetRandomAINavNode()
	{
		var nodes = GetAINavNodes();
		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];
	}

	#endregion
}
