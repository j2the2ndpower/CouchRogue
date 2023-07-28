using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SmartWallTile : Tile {
    [SerializeField] public Sprite[] wallTiles;
    [SerializeField] public GameObject ForegroundPrefab;
    private bool isForeground = false;
    [HideInInspector] public Sprite originalSprite;
    
    /*public override void RefreshTile(Vector3Int position, ITilemap tilemap) {
        if (!neighborUpdate) {
            for (int yd = -1; yd <= 2; yd++) {
                for (int xd = -1; xd <= 1; xd++) {
                    Vector3Int location = new Vector3Int(position.x, position.y + yd, position.z);
                    SmartSmartFloorTile tile = tilemap.GetTile<SmartSmartFloorTile>(position);
                    
                    if (tile != null && tile == this) {
                        tile.neighborUpdate = true;
                        tilemap.RefreshTile(location);
                        tile.neighborUpdate = false;
                    }
                }
            }
        }
    }*/

    public override void GetTileData(Vector3Int pos, ITilemap tilemap, ref TileData tileData) {
        TileBase tileD = GetNonWallTile(new Vector3Int(pos.x, pos.y-1, 0), tilemap);
        TileBase tileR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y, 0), tilemap);
        TileBase tileL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y, 0), tilemap);
        TileBase tileDD = GetNonWallTile(new Vector3Int(pos.x, pos.y-2, 0), tilemap);
        TileBase tileDR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y-1, 0), tilemap);
        TileBase tileDL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y-1, 0), tilemap);
        TileBase tileDDR = GetNonWallTile(new Vector3Int(pos.x+1, pos.y-2, 0), tilemap);
        TileBase tileDDL = GetNonWallTile(new Vector3Int(pos.x-1, pos.y-2, 0), tilemap);

        isForeground = true;

        // 0. Default
        tileData.sprite = wallTiles[0];

        // 1. Two Spaces ABOVE and one space LEFT of top left Floor tile
        if (tileDDR != null && tileDR == null && tileDD == null) {
            tileData.sprite = wallTiles[1];
        }

        // 2. Two Spaces ABOVE Floor tile and Above Wall Tile
        if (tileDD != null && tileD == null) {
            tileData.sprite = wallTiles[2];
        }

        // 3. Two Spaces ABOVE and one space RIGHT of top right Floor tile
        if (tileDDL != null && tileDL == null && tileDD == null) {
            tileData.sprite = wallTiles[3];
        }

        // 4. Directly LEFT UP of floor tile and ABOVE wall tile
        if (tileDR != null && tileD == null) {
            tileData.sprite = wallTiles[4];
        }

        // 5. Directly ABOVE Floor tile
        if (tileD != null) {
            tileData.sprite = wallTiles[5];
            isForeground = false;
        }

        // 6. Directly RIGHT UP of floor tile and ABOVE wall tile
        if (tileDL != null && tileD == null) {
            tileData.sprite = wallTiles[6];
        }

        // 7. Directly LEFT of bottom left Floor tile
        if (tileR != null && tileDR == null) {
            tileData.sprite = wallTiles[7];
        }

        // 8. Directly RIGHT of bottom right Floor tile
        if (tileL != null && tileDL == null) {
            tileData.sprite = wallTiles[8];
        }

        // 9. Two spaces above a floor and up right from a floor
        if (tileDL != null && tileDD != null && tileD == null) {
            tileData.sprite = wallTiles[9];
        }

        // 10. Two spaces above a floor and up left from a floor
        if (tileDR != null && tileDD != null && tileD == null) {
            tileData.sprite = wallTiles[10];
        }

        if (isForeground) {
            tileData.gameObject = ForegroundPrefab;
        }

        tileData.flags = TileFlags.InstantiateGameObjectRuntimeOnly;
        tileData.colliderType = Tile.ColliderType.Grid;
        originalSprite = tileData.sprite;
    }

    public TileBase GetNonWallTile(Vector3Int pos, ITilemap tilemap) {
        TileBase tile = tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0));
        SmartWallTile wall = tilemap.GetTile<SmartWallTile>(new Vector3Int(pos.x, pos.y, 0));
        if (wall != null) { 
            return null;
        } else {
            return tile;
        }
    }
}