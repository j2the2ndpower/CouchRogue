using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTile : ScriptableObject {
    public TileBase tile;
    public CRUnit Occupier = null;
    public bool Occupied = false;

    public void SetTile(TileBase tile) {
        this.tile = tile;
    }
}
