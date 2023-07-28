using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu_Items : RadialMenuItem {
    override public void DoAction() {
        menu.SetActiveScreen("ItemMenu");
    }
}
