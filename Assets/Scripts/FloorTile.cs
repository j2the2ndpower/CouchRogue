using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class FloorTile : Tile {

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/Floor")]
    public static void CreateWall() {
        string path = EditorUtility.SaveFilePanelInProject("Save Tile", "Tile", "asset", "Save Tile", "Assets");
        if (path != "") {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FloorTile>(), path);
        }
    }
#endif
}
