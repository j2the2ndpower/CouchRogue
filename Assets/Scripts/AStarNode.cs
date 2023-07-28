using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode {
    public AStarNode parent = null;
    public Vector2Int position = Vector2Int.zero;
    public float g = 0f;
    public float h = 0f;
    public float f = 0f;

    public AStarNode(AStarNode parent, Vector2Int position) {
        this.parent = parent;
        this.position = position;
    }
}
