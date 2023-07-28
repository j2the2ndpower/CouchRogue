using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class WallTile : Tile {

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/Wall")]
    public static void CreateWall() {
        string path = EditorUtility.SaveFilePanelInProject("Save Tile", "Tile", "asset", "Save Tile", "Assets");
        if (path != "") {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
        }
    }
#endif
}
