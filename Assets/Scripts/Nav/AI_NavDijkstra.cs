using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class AI_NavDijkstra : MonoBehaviour {
    public static bool generate(AI_NavNode startNode, AI_NavNode endNode, ref List<AI_NavNode> path) {
        var nodes = new SimplePriorityQueue<AI_NavNode>();

        startNode.Cost = 0;
        nodes.EnqueueWithoutDuplicates(startNode, startNode.Cost);

        bool found = false;
        while (!found && nodes.Count > 0) {
            var node = nodes.Dequeue();
            if (node == endNode) {
                found = true;
                break;
            }

            foreach (var neighbor in node.neighbors) {
                float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
                if (cost < neighbor.Cost) {
                    neighbor.Cost = cost;
                    neighbor.Parent = node;
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost);
                }
            }
        }

        path.Clear();
        if (found) {
            CreatePathFromParents(endNode, ref path);
        }

        return found;
    }

    public static void CreatePathFromParents(AI_NavNode node, ref List<AI_NavNode> path) {
        // while node not null
        while (node != null) {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.Parent;
        }

        // reverse path
        path.Reverse();
    }
}