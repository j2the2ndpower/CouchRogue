using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu_Hold: RadialMenuItem {
    override public void DoAction() {
        player.remainingActions = 0;
        player.heldActions = 1;
    }

    public void Update() {
        SetEnabled(player.inCombat && player.remainingActions > 0 && game.playerTurn);
    }
}