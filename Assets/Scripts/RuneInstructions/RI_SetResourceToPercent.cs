using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RI_SetResourceToPercent  : RuneInstruction {
    public TargetAlignment targetAlignment;
    public statType resource;
    public float percent;
    
    override public void Attach() {}

    override public void OnCast(List<CRUnit> targets) {
        foreach(CRUnit target in Util.GetTargetsOfType(targetAlignment, targets, caster)) {
            if (!target.statValues.ContainsKey(resource)) continue;
            float newValue = target.maxStatValues[resource] / 100 * percent;
            if (resource == statType.Health) {
                if (newValue >= target.statValues[resource]) {
                    target.heal(newValue - target.statValues[resource], caster, 0f, true);
                } else {
                    target.takeDamage(target.statValues[resource] - newValue, caster, 0f);
                }
            } else {
                if (newValue >= target.statValues[resource]) {
                    target.increaseStat(resource, newValue - target.statValues[resource], false);
                } else {
                    target.reduceStat(resource, target.statValues[resource] - newValue, false);
                }
            }
        }
    }
}
