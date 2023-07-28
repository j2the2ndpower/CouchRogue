using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickup : Collectible {
    public GameObject skillPrefab;
    
    override public void OnInteract(CRPlayer player) {
        if (player.skillMenu.SetSkillAtFirstOpenSelection(skillPrefab)) {
            base.OnInteract(player);
        } else {
            //TODO: Error Noise
        }
    }
}
