using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

abstract public class SelectionMarker : MonoBehaviour {
    protected CRUnit unit;
    protected Vector3Int position;
    [SerializeField] protected Tilemap tilemap;
    [SerializeField] protected Tilemap rangeMap;
    [SerializeField] protected Tile smartSelectionTile;
    protected TargetDisplay targetDisplay;

    protected List<Tile> tiles;

    public void Initialize(CRUnit u, Vector3Int pos, TargetDisplay td) {
        unit = u;
        position = pos;

        targetDisplay = td;
        tilemap.color = unit.unitColor;

        rangeMap.color = new Color(unit.unitColor.r, unit.unitColor.g, unit.unitColor.b, 0.25f);

        CreateMarkers();
        CreateRangeIndicator();
    }
    
    public virtual void CreateMarkers() {

    }

    public virtual void CreateRangeIndicator() {

    }

    public virtual void Move(Vector3Int dir) {
        
    }

    public virtual void DeleteMarkers() {

    }

    public virtual List<CRUnit> GetTargets() {
        return null;
    }
}
