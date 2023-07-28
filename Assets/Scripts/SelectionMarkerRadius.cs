using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SelectionMarkerRadius : SelectionMarker {
    override public void CreateMarkers() {
        base.CreateMarkers();

        tilemap.ClearAllTiles();
        for (int x = Mathf.CeilToInt(-targetDisplay.radius); x <= targetDisplay.radius; x++) {
            for (int y = Mathf.CeilToInt(-targetDisplay.radius); y <= targetDisplay.radius; y++) {
                if (Util.GridDistance(new Vector3Int(x, y, 0)) <= targetDisplay.radius) {
                    bool createTile = unit.map.GetSpace(Vector3Int.FloorToInt(unit.transform.position) + position + new Vector3Int(x, y, 0)) != null;
                    tilemap.SetTile(Vector3Int.FloorToInt(unit.transform.position) + position + new Vector3Int(x,y,0), createTile ? smartSelectionTile : null);
                }
            }
        }
    }

    public override void CreateRangeIndicator() {
        base.CreateRangeIndicator();

        for (int x = Mathf.CeilToInt(-targetDisplay.range); x <= targetDisplay.range; x++) {
            for (int y = Mathf.CeilToInt(-targetDisplay.range); y <= targetDisplay.range; y++) {
                if (Util.GridDistance(new Vector3Int(x, y, 0)) <= targetDisplay.range) {
                    bool createTile = unit.map.GetSpace(Vector3Int.FloorToInt(unit.transform.position) + new Vector3Int(x, y, 0)) != null;
                    rangeMap.SetTile(Vector3Int.FloorToInt(unit.transform.position) + new Vector3Int(x,y,0), createTile ? smartSelectionTile : null);
                }
            }
        }
    }

    public override void Move(Vector3Int dir) {
        base.Move(dir);

        Vector3Int newLocation = Vector3Int.FloorToInt(unit.transform.position) + position + dir;
        bool canMove = (unit.map.GetSpace(newLocation) != null && Util.GridDistance(newLocation-Vector3Int.FloorToInt(unit.transform.position)) <= targetDisplay.range);

        if (canMove) {
            position += dir;
            CreateMarkers();
        }
    }

    public override void DeleteMarkers() {
        tilemap.ClearAllTiles();
        rangeMap.ClearAllTiles();
    }

    public override List<CRUnit> GetTargets() {
        base.GetTargets();
        List<CRUnit> targets = new List<CRUnit>();

        for (int x = Mathf.CeilToInt(-targetDisplay.radius); x <= targetDisplay.radius; x++) {
            for (int y = Mathf.CeilToInt(-targetDisplay.radius); y <= targetDisplay.radius; y++) {
                if (Util.GridDistance(new Vector3Int(x, y, 0)) <= targetDisplay.radius) {
                    Vector3Int spacePos = Vector3Int.FloorToInt(unit.transform.position) + position + new Vector3Int(x, y, 0);
                    CRUnit target = unit.map.GetUnitInSpace(spacePos);

                    if (target != null) {
                        targets.Add(target);
                    }
                }
            }
        }

        return targets;
    }
}
