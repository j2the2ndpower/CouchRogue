using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : Collectible {
    public GameObject relicPrefab;
    override public void OnInteract(CRPlayer player) {
        base.OnInteract(player);
        player.AddRelic(relicPrefab);
    }
}