using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu_Abilities : RadialMenuItem {
    override public void DoAction() {
        menu.SetActiveScreen("SkillMenu");
    }

    public void Update() {
        //SetEnabled(!player.inCombat || (player.inCombat && player.remainingActions > 0));
    }
}
