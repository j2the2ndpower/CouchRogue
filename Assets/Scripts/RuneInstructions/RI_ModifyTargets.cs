using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RI_ModifyTargets : RuneInstruction {
    public TargetAlignment targetAlignment;
    public ModifyAttribute attribute;
    public ModifyOperation operation;
    public float floatValue;
    public int intValue;
    public bool boolValue;
    
    override public void Attach() {}

    override public void OnCast(List<CRUnit> targets) {
        modifyParamaters mParam = new modifyParamaters();
        mParam.attribute = attribute;
        mParam.operation = operation;
        mParam.floatValue = floatValue;
        mParam.intValue = intValue;
        mParam.boolValue = boolValue;
        
        foreach(CRUnit target in Util.GetTargetsOfType(targetAlignment, targets, caster)) {
            target.ModifyUnit(mParam);
        }
    }
}
