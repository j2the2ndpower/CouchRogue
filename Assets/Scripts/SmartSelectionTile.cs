using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

struct TileLookup {
    public uint mustHave;
    public uint mustNotHave;
}

[CreateAssetMenu]
public class SmartSelectionTile : Tile {
    [SerializeField] public Sprite[] tiles;

    private Dictionary<int, TileLookup> tileLookup = new Dictionary<int, TileLookup>() {
        { 0, new TileLookup { mustHave=0b11010000, mustNotHave=0b00001010 } },
        { 1, new TileLookup { mustHave=0b01010010, mustNotHave=0b00001000 } },
        { 2, new TileLookup { mustHave=0b00010110, mustNotHave=0b01001000 } },
        { 3, new TileLookup { mustHave=0b00011000, mustNotHave=0b01000010 } },
        { 4, new TileLookup { mustHave=0b00010000, mustNotHave=0b01001010 } },
        { 5, new TileLookup { mustHave=0b01000010, mustNotHave=0b00011000 } },
        { 6, new TileLookup { mustHave=0b01011000, mustNotHave=0b10100010 } },
        { 7, new TileLookup { mustHave=0b01001000, mustNotHave=0b00110010 } },
        { 8, new TileLookup { mustHave=0b11111000, mustNotHave=0b00000010 } },
        { 9, new TileLookup { mustHave=0b11111111, mustNotHave=0b00000000 } },
        { 10, new TileLookup { mustHave=0b00011010, mustNotHave=0b01000000 } },
        { 11, new TileLookup { mustHave=0b01000000, mustNotHave=0b00011010 } },
        { 12, new TileLookup { mustHave=0b01011010, mustNotHave=0b10100101 } },
        { 13, new TileLookup { mustHave=0b00000010, mustNotHave=0b01011000 } },
        { 14, new TileLookup { mustHave=0b01010010, mustNotHave=0b10001100 } },
        { 15, new TileLookup { mustHave=0b00001010, mustNotHave=0b01010001 } },
        { 16, new TileLookup { mustHave=0b01101000, mustNotHave=0b00010010 } },
        { 17, new TileLookup { mustHave=0b01001010, mustNotHave=0b00010000 } },
        { 18, new TileLookup { mustHave=0b00001011, mustNotHave=0b01010000 } },
        { 19, new TileLookup { mustHave=0b00000000, mustNotHave=0b01011010 } },
        { 20, new TileLookup { mustHave=0b00001000, mustNotHave=0b01010010 } },
        { 21, new TileLookup { mustHave=0b01001010, mustNotHave=0b00110001 } },
        { 22, new TileLookup { mustHave=0b00011010, mustNotHave=0b01000101 } },
        { 23, new TileLookup { mustHave=0b00010010, mustNotHave=0b01001100 } },
        { 24, new TileLookup { mustHave=0b01010000, mustNotHave=0b10001010 } },
        { 25, new TileLookup { mustHave=0b11011111, mustNotHave=0b00100000 } },
        { 26, new TileLookup { mustHave=0b11111110, mustNotHave=0b00000001 } },
        { 27, new TileLookup { mustHave=0b11111011, mustNotHave=0b00000100 } },
        { 28, new TileLookup { mustHave=0b01111111, mustNotHave=0b10000000 } },
        { 29, new TileLookup { mustHave=0b01011111, mustNotHave=0b10100000 } },
        { 30, new TileLookup { mustHave=0b11011110, mustNotHave=0b00100001 } },
        { 31, new TileLookup { mustHave=0b11111010, mustNotHave=0b00000101 } },
        { 32, new TileLookup { mustHave=0b01111011, mustNotHave=0b10000100 } },
        { 33, new TileLookup { mustHave=0b11011011, mustNotHave=0b00100100 } },
        { 34, new TileLookup { mustHave=0b01111110, mustNotHave=0b10000001 } },
        { 35, new TileLookup { mustHave=0b01011011, mustNotHave=0b10100100 } },
        { 36, new TileLookup { mustHave=0b01011110, mustNotHave=0b10100001 } },
        { 37, new TileLookup { mustHave=0b01111010, mustNotHave=0b10000101 } },
        { 38, new TileLookup { mustHave=0b11011010, mustNotHave=0b00100101 } },
        { 39, new TileLookup { mustHave=0b01001011, mustNotHave=0b00110000 } },
        { 40, new TileLookup { mustHave=0b00011110, mustNotHave=0b01000001 } },
        { 41, new TileLookup { mustHave=0b11010010, mustNotHave=0b00001100 } },
        { 42, new TileLookup { mustHave=0b01111000, mustNotHave=0b10000010 } },
        { 43, new TileLookup { mustHave=0b01101010, mustNotHave=0b00010001 } },
        { 44, new TileLookup { mustHave=0b00011011, mustNotHave=0b01000100 } },
        { 45, new TileLookup { mustHave=0b01010110, mustNotHave=0b10001000 } },
        { 46, new TileLookup { mustHave=0b11011000, mustNotHave=0b00100010 } },

    };

    public override void RefreshTile(Vector3Int position, ITilemap tilemap) {
        for (int xd = -1; xd <= 1; xd++) {
            for (int yd = -1; yd <= 1; yd++) {
                Vector3Int location = new Vector3Int(position.x + xd, position.y + yd, position.z);
                if (IsNeighbour(location, tilemap)) {
                    tilemap.RefreshTile(location);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int pos, ITilemap tilemap, ref TileData tileData) {
        uint surroundingTiles = 0;

        uint bit = 0;
        for (int x = pos.x-1; x <= pos.x+1; x++) {
            for (int y = pos.y+1; y >= pos.y-1; y--) {
                if (!(x == pos.x && y == pos.y)) {
                    TileBase tile = GetOtherTile(new Vector3Int(x, y, 0), tilemap);
                    if (tile != null) {
                        surroundingTiles = surroundingTiles + (uint)(Mathf.Pow(2, bit));
                    }
                    bit++;
                }
            }
        }

        foreach(KeyValuePair<int, TileLookup> req in tileLookup) {
            if ((req.Value.mustHave & surroundingTiles) == req.Value.mustHave && ((req.Value.mustNotHave ^ surroundingTiles) & req.Value.mustNotHave) == req.Value.mustNotHave) {
                tileData.sprite = tiles[req.Key];
            }
        }

        tileData.flags = TileFlags.None;
        tileData.colliderType = Tile.ColliderType.None;
    }

    public TileBase GetOtherTile(Vector3Int pos, ITilemap tilemap) {
        SmartSelectionTile mmt = tilemap.GetTile<SmartSelectionTile>(new Vector3Int(pos.x, pos.y, 0));
        return mmt;
    }

    private bool IsNeighbour(Vector3Int position, ITilemap tilemap)
    {
        TileBase tile = tilemap.GetTile(position);
        return (tile != null && tile == this);
    }
}