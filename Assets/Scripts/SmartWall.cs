using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SmartWall : MonoBehaviour {
    [SerializeField] public Sprite[] wallTiles;

    private SpriteRenderer spriteRenderer;
    private Map map;
    private Tilemap tilemap;
    private Grid grid;
    private Vector3Int pos;

    // Start is called before the first frame update
    void Start()  {
        spriteRenderer = GetComponent<SpriteRenderer>();
        grid = GetComponentInParent<Grid>();
        pos = grid.WorldToCell(transform.position);
        tilemap = GetComponentInParent<Tilemap>();
        
        FloorTile tileD = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x, pos.y-1, 0));
        FloorTile tileR = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x+1, pos.y, 0));
        FloorTile tileL = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x-1, pos.y, 0));
        FloorTile tileDD = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x, pos.y-2, 0));
        FloorTile tileDR = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x+1, pos.y-1, 0));
        FloorTile tileDL = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x-1, pos.y-1, 0));
        FloorTile tileDDR = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x+1, pos.y-2, 0));
        FloorTile tileDDL = tilemap.GetTile<FloorTile>(new Vector3Int(pos.x-1, pos.y-2, 0));

        // 0. Default
        spriteRenderer.sprite = wallTiles[0];

        // 1. Two Spaces ABOVE and one space LEFT of top left Floor tile
        if (tileDDR != null && tileDR == null && tileDD == null) {
            spriteRenderer.sprite = wallTiles[1];
        }

        // 2. Two Spaces ABOVE Floor tile and Above Wall Tile
        if (tileDD != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[2];
        }

        // 3. Two Spaces ABOVE and one space RIGHT of top right Floor tile
        if (tileDDL != null && tileDL == null && tileDD == null) {
            spriteRenderer.sprite = wallTiles[3];
        }

        // 4. Directly LEFT UP of floor tile and ABOVE wall tile
        if (tileDR != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[4];
        }

        // 5. Directly ABOVE Floor tile
        if (tileD != null) {
            spriteRenderer.sprite = wallTiles[5];
            spriteRenderer.sortingOrder = -1;
        }

        // 6. Directly RIGHT UP of floor tile and ABOVE wall tile
        if (tileDL != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[6];
        }

        // 7. Directly LEFT of bottom left Floor tile
        if (tileR != null && tileDR == null) {
            spriteRenderer.sprite = wallTiles[7];
        }

        // 8. Directly RIGHT of bottom right Floor tile
        if (tileL != null && tileDL == null) {
            spriteRenderer.sprite = wallTiles[8];
        }

        // 9. Two spaces above a floor and up right from a floor
        if (tileDL != null && tileDD != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[9];
        }

        // 10. Two spaces above a floor and up left from a floor
        if (tileDR != null && tileDD != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[10];
        }
    }
}
