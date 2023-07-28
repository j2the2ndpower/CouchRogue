using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenu_Move : RadialMenuItem {

    override public void DoAction() {
        if (player.inCombat) {
            player.startMove();
        }
    }

    public void Update() {
        SetEnabled(player.inCombat && player.remainingActions > 0 && game.playerTurn);
    }
}
