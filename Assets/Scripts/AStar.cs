using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A* (star) Pathfinding
public class AStar {
    // Initialize both open and closed list
    List<AStarNode> openList = new List<AStarNode>();
    List<AStarNode> closedList = new List<AStarNode>();
    public List<Vector2Int> path = new List<Vector2Int>();

    public AStar(Map maze, Vector2Int start, Vector2Int end) {
        // Add the start node
        openList.Add(new AStarNode(null, start));

        // Loop until you find the end
        while (openList.Count > 0) {
            // Get the current node
            AStarNode currentNode = null;
            int currentIndex = 0;

            for (int i = 0; i < openList.Count; i++) {
                if (currentNode == null || openList[i].f < currentNode.f) {
                    currentNode = openList[i];
                    currentIndex = i;
                }
            }

            openList.RemoveAt(currentIndex);
            closedList.Add(currentNode);

            // Found the goal
            if (currentNode.position == end) {
                while (currentNode != null) {
                    path.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }
                return;
            }

            // Generate children
            List<AStarNode> children = new List<AStarNode>();
            List<Vector2Int> adjacentSquares = new List<Vector2Int>() {
                new Vector2Int(0, -1),
                new Vector2Int(0, 1),
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(1, 1)
            };

            foreach (Vector2Int adjacentSquare in adjacentSquares) {
                Vector2Int nodePosition = adjacentSquare + currentNode.position;
                if (!maze.MapHasTile(new Vector3Int(nodePosition.x, nodePosition.y, 0))) {
                    continue;
                }

                if (maze.IsSpaceOccupied(new Vector3Int(nodePosition.x, nodePosition.y, 0))) {
                    continue;
                }

                children.Add(new AStarNode(currentNode, nodePosition));
            }

            foreach (AStarNode child in children) {
                bool alreadyClosed = false;
                foreach (AStarNode closedChild in closedList) {
                    if (closedChild.position == child.position) {
                        alreadyClosed = true;
                    }
                }

                if (alreadyClosed) {
                    continue;
                }

                //Calculate f g and h
                child.g = currentNode.g + ((child.position - currentNode.position).magnitude > 1 ? 1.5f : 1);
                child.h = Util.GridDistance(new Vector3Int(child.position.x, child.position.y, 0) - new Vector3Int(end.x, end.y, 0));
                child.f = child.g + child.h;

                bool alreadyOpened = false;
                foreach (AStarNode openChild in openList) {
                    if (openChild.position == child.position) {
                        alreadyOpened = true;
                    }
                }

                if (alreadyOpened) {
                    continue;
                }

                openList.Add(child);
            }
        }
    }
}
