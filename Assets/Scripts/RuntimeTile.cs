using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RuntimeTile : MonoBehaviour {
    void Start() {
        Tilemap tilemap = GetComponent<Tilemap>();
        //SmartFloorTile tile = tilemap.GetTile<SmartFloorTile>(new Vector3Int(0, 0, 0));
        //TileData td = tile.GetTileData();
        //td.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = 4;
        //td.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
