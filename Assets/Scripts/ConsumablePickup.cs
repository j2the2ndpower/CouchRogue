using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePickup : Collectible {
    public GameObject skillPrefab;
    
    override public void OnInteract(CRPlayer player) {
        if (player.consumableMenu.SetSkillAtFirstOpenSelection(skillPrefab)) {
            base.OnInteract(player);
        } else {
            //TODO: Error Noise
        }
    }
}
