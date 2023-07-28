using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private CRPlayer player;

    void Start() {
        player = GetComponentInParent<CRPlayer>();
    }
    
    public void StopBlinking() {
        player.isBlinking = false;
    }

    public void StopAttacking() {
        player.StopBasicAttack();
    }
}
