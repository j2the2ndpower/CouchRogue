using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DunGen;

public class MapSpace {
    public CRUnit Occupier;
    public List<CRUnit> unitsSelecting = new List<CRUnit>();
    public List<CRUnit> unitsAnchored = new List<CRUnit>();
    public List<CRUnit> unitsHighlighting = new List<CRUnit>();
}

public class Map : MonoBehaviour {
    [SerializeField] private GameObject marker;
    [HideInInspector] public RuntimeDungeon dungeon;
    [HideInInspector] private List<Tilemap> tilemaps = new List<Tilemap>();
    private Dictionary<int, Dictionary<int, MapSpace>> spaces = new Dictionary<int, Dictionary<int, MapSpace>>();
    private Dictionary<int, Dictionary<int, Marker>> activeSpaces = new Dictionary<int, Dictionary<int, Marker>>();

    private void Start() {
        dungeon = FindObjectOfType<RuntimeDungeon>();
        dungeon.Generator.OnGenerationStatusChanged += OnGenerationStatusChange;
    }

    public void OnGenerationStatusChange(DungeonGenerator g, GenerationStatus status) {
        if (status == GenerationStatus.Complete) {
            foreach(Tilemap tm in GetComponentsInChildren<Tilemap>()) {
                if (tm.gameObject.layer == 0) {
                    tilemaps.Add(tm);
                    for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++) {
                        for (int y = tm.cellBounds.yMin; y <= tm.cellBounds.yMax; y++) {
                            TileBase tb = tm.GetTile<TileBase>(new Vector3Int(x, y, 0));
                            if (tb is SmartFloorTile && !((SmartFloorTile)tb).isFake) {
                                int tileX = Mathf.FloorToInt(tm.transform.position.x) + x;
                                int tileY = Mathf.FloorToInt(tm.transform.position.y) + y;
                                if (!spaces.ContainsKey(tileX)) {
                                    spaces.Add(tileX, new Dictionary<int, MapSpace>());
                                }
                                if (!spaces[tileX].ContainsKey(tileY)) {
                                    spaces[tileX].Add(tileY, new MapSpace());
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public bool IsSpaceOccupied(Vector3Int coords) {
        if (!spaces.ContainsKey(coords.x) || !spaces[coords.x].ContainsKey(coords.y)) {
            return true;
        } 
        return spaces[coords.x][coords.y].Occupier != null;
    }

    public void SetSpaceOccupied(Vector3Int coords, CRUnit occupier) {
        if (!MapHasTile(coords)) return;
        
        if (spaces.ContainsKey(coords.x) && spaces[coords.x].ContainsKey(coords.y)) {
            MapSpace space = spaces[coords.x][coords.y];
            space.Occupier = occupier;
        }
    }

    public bool MapHasTile(Vector3Int coords) {
        UnityEngine.Tilemaps.TileBase tile = null;
        foreach(Tilemap tilemap in tilemaps) {
            tile = tilemap.GetTile(tilemap.WorldToCell(coords));
            if (tile != null) {
                break;
            }
        }

        return tile != null;
    }

    public CRUnit GetUnitInSpace(Vector3Int coords) {
        if (spaces.ContainsKey(coords.x) && spaces[coords.x].ContainsKey(coords.y)) {
            MapSpace space = spaces[coords.x][coords.y];
            return space.Occupier;
        }

        return null;
    }

    public Vector2Int findRandomEmptySpace(Vector2Int start, Vector2Int stop, int range, int monsterCount) {
        int startX = Mathf.Min(start.x, stop.x);
        int stopX = Mathf.Max(start.x, stop.x);
        int startY = Mathf.Min(start.y, stop.y);
        int stopY = Mathf.Max(start.y, stop.y);
        List<Vector2Int> validLocations = new List<Vector2Int>();
        for (int x = startX; x <= stopX; x++) {
            for (int y = startY; y <= stopY; y++) {
                int counter = 0;
                for (int xx = x; xx <= x+range; xx++) {
                    for (int yy = y; yy <= y+range; yy++) {
                        if (MapHasTile(new Vector3Int(xx, yy, 0)) && !IsSpaceOccupied(new Vector3Int(xx,yy,0))) {
                            counter++;
                            if (counter >= monsterCount) {
                                validLocations.Add(new Vector2Int(x, y));
                                break;
                            }
                        }
                    }
                    if (counter >= monsterCount) break;
                }
            }
        }

        return validLocations[Random.Range(0, validLocations.Count)];
    }

    public Vector3 GetRandomFreeLocationInRadius(Vector3 position, float radius) {
        List<Vector3Int> validLocations = new List<Vector3Int>();
        for (int x = Mathf.FloorToInt(position.x - radius); x <= Mathf.FloorToInt(position.x + radius); x++) {
            for (int y = Mathf.FloorToInt(position.y - radius); y <= Mathf.FloorToInt(position.y + radius); y++) {
                Vector3Int testCoords = new Vector3Int(x,y,0);
                if (Util.GridDistance(Vector3Int.FloorToInt(position - testCoords)) <= radius && !IsSpaceOccupied(testCoords)) {
                    validLocations.Add(testCoords);
                }
            }
        }
        return validLocations[Random.Range(0, validLocations.Count)];
    }

    public MapSpace GetSpace(Vector3 position) {
        Vector3Int pos = Vector3Int.FloorToInt(position);
        if (spaces.ContainsKey(pos.x) && spaces[pos.x].ContainsKey(pos.y)) {
            return spaces[pos.x][pos.y];
        }
        return null;
    }

    public Marker GetMarker(Vector3 position) {
        Vector3Int pos = Vector3Int.FloorToInt(position);
        if (activeSpaces.ContainsKey(pos.x) && activeSpaces[pos.x].ContainsKey(pos.y)) {
            //print("returning " + pos.x + "," + pos.y);
            return activeSpaces[pos.x][pos.y];
        }
        //print("returning null");
        return null;
    }

    public Marker SetMarkerActive(Vector3Int position, CRUnit unit, bool active, bool HideWhenOccupied = false) {
        Marker m = null;
        if (!(spaces.ContainsKey(position.x) && spaces[position.x].ContainsKey(position.y))) {
            return null;
        }

        if (active) {
            if (activeSpaces.ContainsKey(position.x) && activeSpaces[position.x].ContainsKey(position.y)) {
                m = activeSpaces[position.x][position.y];
            } else {
                if (!activeSpaces.ContainsKey(position.x)) {
                    activeSpaces.Add(position.x, new Dictionary<int, Marker>());
                }
            }

            //create marker
            if (m == null) {
                GameObject ob = Instantiate(marker, position, Quaternion.identity);
                m = ob.GetComponent<Marker>();
                m.SetColor(unit.unitColor);
                m.HideWhenOccupied = HideWhenOccupied;
                ob.transform.SetParent(this.transform);
                activeSpaces[position.x].Add(position.y, m);
            }
        }

        //update space
        if (!spaces[position.x][position.y].unitsHighlighting.Contains(unit) && active == true) {
            spaces[position.x][position.y].unitsHighlighting.Add(unit);
        }

        if (spaces[position.x][position.y].unitsHighlighting.Contains(unit) && active == false) {
            spaces[position.x][position.y].unitsHighlighting.Remove(unit);
            
            if ( spaces[position.x][position.y].unitsHighlighting.Count == 0 ) {
                activeSpaces[position.x][position.y].gameObject.SetActive(false);
                Destroy(activeSpaces[position.x][position.y].gameObject);
                activeSpaces[position.x].Remove(position.y);
                if (activeSpaces[position.x].Count == 0) {
                    activeSpaces.Remove(position.x);
                }
            }
        }

        return m;
    }

    public void SetSpaceSelected(Vector3Int position, bool selected, CRUnit unit, float range) {
        if (!spaces.ContainsKey(position.x) || !spaces[position.x].ContainsKey(position.y))
            return;

        MapSpace space = GetSpace(position);

        if (space != null) {
            if (selected) {
                space.unitsSelecting.Add(unit);
            } else {
                space.unitsSelecting.Remove(unit);
                if (Util.GridDistance(unit.gridPosition-position) > range) {
                    SetMarkerActive(position, unit, false);
                }
            }
        }
    }

    public void CreateSelectionRadius(Vector3Int anchor, CRUnit unit, float range, float radius) {
        for (int x = Mathf.CeilToInt(-range); x <= range; x++) {
            for (int y = Mathf.CeilToInt(-range); y <= range; y++) {
                Vector3Int newMarkerPos = new Vector3Int(anchor.x+x, anchor.y+y, 0);
                Marker marker = GetMarker(newMarkerPos);
                MapSpace space = GetSpace(newMarkerPos);
                /*if (Util.GridDistance(new Vector3Int(x, y, 0)) <= radius) {
                    print("creating at " + newMarkerPos);
                    if (marker == null) {
                        marker = SetMarkerActive(newMarkerPos, unit, true, false);
                    } if (space != null) {
                        SetSpaceSelected(newMarkerPos, true, unit, range); 
                        if (newMarkerPos == anchor) GetSpace(newMarkerPos).SelectionAnchor = true;
                    }*/
                if (Util.GridDistance(new Vector3Int(x, y, 0)) <= range) {
                    if (marker == null || !space.unitsHighlighting.Contains(unit)) {
                        marker = SetMarkerActive(newMarkerPos, unit, true, false);
                    } if (space != null) {
                        unit.highlightedSpaces.Add(marker);
                        if (newMarkerPos == anchor) { 
                            SetSelectionAnchor(newMarkerPos, true, unit);
                            if (!unit.selectedSpaces.Contains(marker)) unit.selectedSpaces.Add(marker);
                        }
                    }
                }
            }
        }

        List<Vector3Int> Test = new List<Vector3Int>();
        foreach(Marker m in unit.selectedSpaces) {
            Test.Add(Vector3Int.FloorToInt(m.transform.position));
        }
        MoveSelectedSpaces(Test, new Vector3Int(0,0,0), unit, range, radius);
    }

    public bool MoveSelectedSpaces(List<Vector3Int> pos, Vector3Int moveInt, CRUnit unit, float range, float radius) {
        Vector3Int anchorPosition = new Vector3Int();

        //get rid of all the non-anchor selectors
        for(int i = pos.Count-1; i >= 0; i--) {
            MapSpace currentSpace = GetSpace(pos[i]);
            if (pos[i] == unit.anchorSpace) {
                anchorPosition = pos[i];
            } else {
                SetSpaceSelected(pos[i], false, unit, range);
                unit.selectedSpaces.RemoveAt(i);
            }
        }

        //move anchor in direction (if you can)
        Vector3Int newPosition = anchorPosition + moveInt;
        bool canMove = (GetSpace(newPosition) != null && Util.GridDistance(newPosition-unit.gridPosition) <= range);

        if (canMove) {
            SetSpaceSelected(anchorPosition, false, unit, range);
            SetSelectionAnchor(anchorPosition, false, unit);
            unit.selectedSpaces.Remove(GetMarker(anchorPosition));

            anchorPosition = newPosition;

            SetSpaceSelected(anchorPosition, true, unit, range);
            SetSelectionAnchor(anchorPosition, true, unit);
            unit.selectedSpaces.Add(GetMarker(anchorPosition));          
        }

        //recreate around the anchor
        for (int x = Mathf.CeilToInt(-radius); x <= radius; x++) {
            for (int y = Mathf.CeilToInt(-radius); y <= radius; y++) {
                if (Util.GridDistance(new Vector3Int(x, y, 0)) <= radius) {
                    Vector3Int newMarkerPos = new Vector3Int(anchorPosition.x+x, anchorPosition.y+y, 0);
                    Marker marker = GetMarker(newMarkerPos);

                    if (marker == null) {
                        marker = SetMarkerActive(newMarkerPos, unit, true, false);
                    } if (newMarkerPos != anchorPosition) {
                        SetSpaceSelected(newMarkerPos, true, unit, range); 
                        if (marker != null && !unit.selectedSpaces.Contains(marker)) unit.selectedSpaces.Add(marker);
                    }  
                }
            }
        }        

        /*List<MapSpace> mapSpaces = new List<MapSpace>();

        //Remove Spaces as selected
        foreach (Vector3Int p in pos) {
            MapSpace ms = GetSpace(p);
            mapSpaces.Add(ms);
            player.selectedSpaces.Remove(GetMarker(p));
            ms.Selected = false;
        }

        //Check if desired locations are all markers
        List<Vector3Int> newPositions = new List<Vector3Int>();
        bool newLocationIsAvailable = true;

        for (int i = 0; i < pos.Count; i++) {
            newPositions.Add(pos[i]);
            newPositions[i] += moveInt;
            if (GetMarker(newPositions[i]) == null) {
                newLocationIsAvailable = false;
                break;
            }
        }

        //Shift positions in direction
        if (newLocationIsAvailable) {
            for (int i = 0; i < pos.Count; i++) {
                pos[i] = newPositions[i];
            }
        }

        //Add Spaces as selected
        foreach (Vector3Int p in pos) {
            MapSpace space = GetSpace(p);
            space.Selected = true;
            player.selectedSpaces.Add(GetMarker(p));
        }*/

        return canMove;
    }

    public void SetSelectionAnchor(Vector3Int position, bool b, CRUnit unit) {
        MapSpace space = GetSpace(position);
        if (space != null && b) { 
            space.unitsAnchored.Add(unit);
            unit.anchorSpace = position; 
        } else if (space != null) {
            space.unitsAnchored.Remove(unit);
        }
    }
}
