using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePickup : Collectible {
    public RuneType type;
    
    override public void OnInteract(CRPlayer player) {
        player.menu.ShowRunePreview(type, this);
    }
}