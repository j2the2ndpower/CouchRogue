
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Remove : StatusEffectInstruction {
    override public void OnEvent(effectParamaters param) {
        owner.RemoveStatusEffect(statusEffect);
    }
}
