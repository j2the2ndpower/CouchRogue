using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    private Map map;
    private Instruction[] instructions;

    private Vector2Int roomSize;
    private Vector2Int bottomLeft;

    public Vector2Int RoomSize { get => roomSize; set => roomSize=value; }
    public Vector2Int BottomLeft { get => bottomLeft; set => bottomLeft=value; }

    // Start is called before the first frame update
    void Start() {
        map = transform.parent.GetComponent<Map>();
        DunGen.Tile tile = GetComponent<DunGen.Tile>();
        roomSize = new Vector2Int((int)tile.Bounds.extents.x*2, (int)tile.Bounds.extents.y*2);
        bottomLeft = new Vector2Int((int)(transform.position.x + tile.Bounds.center.x - tile.Bounds.extents.x),
                                    (int)(transform.position.y + tile.Bounds.center.y - tile.Bounds.extents.y));

        instructions = GetComponents<Instruction>();
        foreach (Instruction instruction in instructions) {
            instruction.SetMap(map);
            instruction.Perform();
        }
    }
}
