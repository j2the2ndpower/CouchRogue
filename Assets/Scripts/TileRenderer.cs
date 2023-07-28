using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRenderer : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Grid grid;
    private Vector3Int pos;
    private Tilemap tilemap;

    [SerializeField] public Sprite[] wallTiles;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        grid = GetComponentInParent<Grid>();
        pos = grid.LocalToCell(transform.localPosition);
        tilemap = GetComponentInParent<Tilemap>();

        SmartFloorTile floorTile = tilemap.GetTile<SmartFloorTile>(pos);
        if (floorTile) { setFloorSprite(tilemap, pos); }

        SmartWallTile wallTile = tilemap.GetTile<SmartWallTile>(pos);
        if (wallTile) { setWallSprite(tilemap, pos); }
    }

    void setFloorSprite(Tilemap tilemap, Vector3Int pos) {
        SmartFloorTile tileD =  tilemap.GetTile<SmartFloorTile>(new Vector3Int(pos.x, pos.y-1, 0));
        SmartFloorTile tileDR = tilemap.GetTile<SmartFloorTile>(new Vector3Int(pos.x+1, pos.y-1, 0));
        SmartFloorTile tileDL = tilemap.GetTile<SmartFloorTile>(new Vector3Int(pos.x-1, pos.y-1, 0));

        // 0. Up left from floor and above wall
        if (tileDR != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[11];
        }

        // 1. Up right from floor and above wall
        if (tileDL != null && tileD == null) {
            spriteRenderer.sprite = wallTiles[12];
        }

        // 2. above 3 adjacent walls
        if (tileDR == null && tileD == null && tileDL == null) {
            spriteRenderer.sprite = wallTiles[13];
        }
    }

    void setWallSprite(Tilemap tilemap, Vector3Int pos) {
        TileBase tileD = GetNonWallTile(new Vector3Int(pos.x, pos.y-1, 0), tilemap);
        TileBase tileR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y, 0), tilemap);
        TileBase tileL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y, 0), tilemap);
        TileBase tileDD = GetNonWallTile(new Vector3Int(pos.x, pos.y-2, 0), tilemap);
        TileBase tileDR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y-1, 0), tilemap);
        TileBase tileDL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y-1, 0), tilemap);
        TileBase tileDDR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y-2, 0), tilemap);
        TileBase tileDDL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y-2, 0), tilemap);

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

    public TileBase GetNonWallTile(Vector3Int pos, Tilemap tilemap) {
        TileBase tile = tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0));
        SmartWallTile wall = tilemap.GetTile<SmartWallTile>(new Vector3Int(pos.x, pos.y, 0));
        if (wall != null) { 
            return null;
        } else {
            return tile;
        }
    }
}
