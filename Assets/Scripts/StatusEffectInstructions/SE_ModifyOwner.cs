using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifyAttribute {
    None,
    Armor,
    Strength,
    RemainingActions,
    Stunned,
    Dead,
    Shield,
}
public enum StatusEffectAttribute {
    None,
    Duration,
    Magnitude,
    Frequency, 
}

public class SE_ModifyOwner : StatusEffectInstruction {
    public ModifyAttribute attribute;
    public ModifyOperation operation;
    public float floatValue;
    public int intValue;
    public bool boolValue;
    public StatusEffectAttribute listenAttribute;
    


    override public void OnEvent(effectParamaters param) {
        modifyParamaters mParam = new modifyParamaters();
        mParam.attribute = attribute;
        mParam.operation = operation;
        mParam.floatValue = floatValue*statusEffect.magnitude;
        mParam.intValue = intValue*statusEffect.magnitude;
        mParam.boolValue = boolValue;
        owner.ModifyUnit(mParam);
    }
}
