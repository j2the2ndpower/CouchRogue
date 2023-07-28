using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {
    public float distance;
    private SpriteRenderer spriteRenderer;
    private float breathSpeed = 1f;
    private bool gettingBrighter = true;
    private float change = 0f;
    public List<Color> MarkerColors = new List<Color>();
    [SerializeField] private float maxVal = 0.9f;
    [SerializeField] private float minVal = 0.65f;
    [SerializeField] private List<Transform> playerGroups;
    [SerializeField] private List<SpriteRenderer> selectors;
    [HideInInspector] public bool path = false;
    public bool HideWhenOccupied = true;
    [HideInInspector] public GameObject lineMarker;
    public MapSpace space;
    private Map map;

    public void Start() {
        map = FindObjectOfType<Map>();
    }

    public void Update() {
        space = map.GetSpace(transform.position);
        if (space == null) { return; }

        MarkerColors = GetColors(space.unitsHighlighting);
        SpriteRenderer[] spriteRenderers = playerGroups[MarkerColors.Count-1].GetComponentsInChildren<SpriteRenderer>();
        change += (maxVal - minVal) * Time.deltaTime * breathSpeed;

        for (int t = 0; t < playerGroups.Count; t++) {
            bool active = (t == MarkerColors.Count-1);
            if (HideWhenOccupied && map.IsSpaceOccupied(Vector3Int.FloorToInt(transform.position))) active = false;
            playerGroups[t].gameObject.SetActive(active);
        }

        for (int c = 0; c < MarkerColors.Count; c++) {
            if (gettingBrighter) {
                spriteRenderers[c].color = new Color(MarkerColors[c].r, MarkerColors[c].g, MarkerColors[c].b, minVal + change);
                if (minVal + change >= maxVal) { gettingBrighter = false; change = 0f; }
            } else {
                spriteRenderers[c].color = new Color(MarkerColors[c].r, MarkerColors[c].g, MarkerColors[c].b, maxVal - change);
                if (maxVal - change <= minVal) { gettingBrighter = true; change = 0f; }
            }
        }

        SetSelectorActive(space.unitsSelecting.Count);
    }

    private List<Color> GetColors(List<CRUnit> units) {
        SortedList<int, Color> colors = new SortedList<int, Color>();


        foreach (CRUnit unit in units) {
            int index = 99;
            CRPlayer player = unit.GetComponent<CRPlayer>();
            if (player != null) {
                index = player.playerInfo.playerIndex;
            }
            if (!colors.ContainsKey(index)) {
                colors.Add(index, unit.unitColor);
            }
        }

        //Sanity check?
        if (colors.Count == 0) {
            colors.Add(0, Color.white);
        }

        return new List<Color>(colors.Values);
    }

    public void SetDistance(int x, int y) {
        distance = Util.GridDistance(new Vector3Int(x, y, 0));
    }

    public void SetColor(Color color) {
        //MarkerColors.Add(color);
    }

    public void SetSelectorActive(int count) {
        for (int i = 0; i < selectors.Count; i++) {
            selectors[i].gameObject.SetActive((i < count));
            if (i < count) selectors[i].color = space.unitsSelecting[i].unitColor;
        }
    }
}
