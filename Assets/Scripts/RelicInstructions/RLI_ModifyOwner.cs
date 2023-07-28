using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLI_ModifyOwner : RelicInstruction {
    public ModifyAttribute attribute;
    public ModifyOperation operation;
    public float floatValue;
    public int intValue;
    public bool boolValue;

    override public void Perform(RelicEventArgs eventArgs) {
        base.Perform(eventArgs);

        modifyParamaters mParam = new modifyParamaters();
        mParam.attribute = attribute;
        mParam.operation = operation;
        mParam.floatValue = floatValue;
        mParam.intValue = intValue;
        mParam.boolValue = boolValue;

        relic.owner.ModifyUnit(mParam);
    }
}