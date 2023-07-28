using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SmartFloorTile : Tile {
    [SerializeField] public Sprite[] spriteList;
    [SerializeField] public int[] weightList;
    [SerializeField] public Sprite[] wallTiles;
    [SerializeField] public GameObject ForegroundPrefab;
    [SerializeField] public bool isFake = false;
    [SerializeField] public Sprite fakeSprite;
    [HideInInspector] public Sprite originalSprite;

    private bool hasWall = false;
    private bool initialized = false;

    public bool neighborUpdate = false;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go) {
        if (Application.isPlaying) {
            RefreshTile(position, tilemap);
        }
        return base.StartUp(position, tilemap, go);
    }

    /*public override void RefreshTile(Vector3Int position, ITilemap tilemap) {
        if (!neighborUpdate) {
            for (int yd = -1; yd <= 2; yd++) {
                for (int xd = -1; xd <= 1; xd++) {
                    Vector3Int location = new Vector3Int(position.x, position.y + yd, position.z);
                    SmartFloorTile tile = tilemap.GetTile<SmartFloorTile>(position);
                    
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
        SmartWallTile wallD = tilemap.GetTile<SmartWallTile>(new Vector3Int(pos.x, pos.y-1, 0));
        SmartWallTile wallDR = tilemap.GetTile<SmartWallTile>(new Vector3Int(pos.x+1, pos.y-1, 0));
        SmartWallTile wallDL = tilemap.GetTile<SmartWallTile>(new Vector3Int(pos.x-1, pos.y-1, 0));
        SmartFloorTile floorL = tilemap.GetTile<SmartFloorTile>(new Vector3Int(pos.x-1, pos.y, 0));
        SmartFloorTile floorR = tilemap.GetTile<SmartFloorTile>(new Vector3Int(pos.x+1, pos.y, 0));

        bool wasWall = hasWall;
        hasWall = false;

        // 0. Up left from floor and above wall
        if (wallDR == null && wallD != null) {
            tileData.sprite = wallTiles[0];
            hasWall = true;
        }

        // 1. Up right from floor and above wall
        if (wallDL == null && wallD != null) {
            tileData.sprite = wallTiles[1];
            hasWall = true;
        }

        // 1.5: Cap of Vertical column of wall tiles
        if (wallDR == null && wallDL == null && wallD != null) {
            if (floorL != null && floorL.isFake) {
                tileData.sprite = wallTiles[0];
            }

            if (floorR != null && floorR.isFake) {
                tileData.sprite = wallTiles[1];
            }
        }

        // 2. above 3 adjacent walls
        if (wallDR != null && wallD != null && wallDL != null) {
            tileData.sprite = wallTiles[2];
            hasWall = true;
        }

        if ( !hasWall && (!initialized || wasWall) && spriteList.Length > 0) {
            //Debug.Log("choosing random sprite:" + Util.WeightRandom(weightList));
            tileData.sprite = spriteList[Util.WeightRandom(weightList)];
            //tileData.sprite = spriteList[0];
        }

        if (isFake) {
            if (Application.isPlaying) {
                tileData.sprite = null;
            } else {
                tileData.color = new Color(1, 0, 0, 0.5f);
            }
        }

        if (hasWall) {
            tileData.gameObject = ForegroundPrefab;
        }
        tileData.colliderType = colliderType;
        tileData.flags = TileFlags.InstantiateGameObjectRuntimeOnly;
    }
}