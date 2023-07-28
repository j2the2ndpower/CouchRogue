using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomSprite : MonoBehaviour {
    [SerializeField] public Sprite[] spriteList;
    [SerializeField] public int[] weightList;
    [SerializeField] public Sprite[] wallTiles;
    private SpriteRenderer spriteRenderer;
    private Map map;
    private Tilemap tilemap;
    private Grid grid;
    private Vector3Int pos;
    private bool hasWall = false;

    void Start() {
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


        // 0. Up left from floor and above wall
        if (tileDR != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[0];
            hasWall = true;
        }

        // 1. Up right from floor and above wall
        if (tileDL != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[1];
            hasWall = true;
        }

        // 2. above 3 adjacent walls
        if (tileDR == null && tileD == null && tileDL == null) {
            spriteRenderer.sprite = wallTiles[2];
            hasWall = true;
        }

        if (!hasWall && spriteList.Length > 0) {
            GetComponent<SpriteRenderer>().sprite = spriteList[Util.WeightRandom(weightList)];
        }
    }
}
