using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SmartMiniMapTile : Tile {
    [SerializeField] public Sprite[] tiles;

    public override void GetTileData(Vector3Int pos, ITilemap tilemap, ref TileData tileData) {
        TileBase tileD = GetOtherTile(new Vector3Int(pos.x, pos.y-1, 0), tilemap);
        TileBase tileR = GetOtherTile(new Vector3Int(pos.x+1, pos.y, 0), tilemap);
        TileBase tileL = GetOtherTile(new Vector3Int(pos.x-1, pos.y, 0), tilemap);
        TileBase tileU = GetOtherTile(new Vector3Int(pos.x, pos.y+1, 0), tilemap);
        TileBase tileDR = GetOtherTile(new Vector3Int(pos.x+1, pos.y-1, 0), tilemap);
        TileBase tileDL = GetOtherTile(new Vector3Int(pos.x-1, pos.y-1, 0), tilemap);

        // 0. Default
        tileData.sprite = tiles[0];

        // 1. Top Middle
        if (tileU == null) {
            tileData.sprite = tiles[1];
        }

        // 2. Left Middle
        if (tileL == null) {
            tileData.sprite = tiles[2];
        }

        // 3. Right Middle
        if (tileR == null) {
            tileData.sprite = tiles[3];
        }

        // 4. Bottom Middle
        if (tileD == null) {
            tileData.sprite = tiles[4];
        }

        // 5. Top Left
        if (tileU == null && tileL == null) {
            tileData.sprite = tiles[5];
        }

        // 6. Top Right
        if (tileU == null && tileR == null) {
            tileData.sprite = tiles[6];
        }

        // 7. Bottom Left
        if (tileL == null && tileD == null) {
            tileData.sprite = tiles[7];
        }

        // 8. Bottom Right
        if (tileR == null && tileD == null) {
            tileData.sprite = tiles[8];
        }

        tileData.flags = TileFlags.InstantiateGameObjectRuntimeOnly;
        tileData.colliderType = Tile.ColliderType.None;
    }

    public TileBase GetOtherTile(Vector3Int pos, ITilemap tilemap) {
        SmartMiniMapTile mmt = tilemap.GetTile<SmartMiniMapTile>(new Vector3Int(pos.x, pos.y, 0));
        return mmt;
    }
}