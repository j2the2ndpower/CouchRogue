using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLI_HealTarget : RelicInstruction {
    public RelicTarget target;
    public RelicValueType valueType;
    public float amount = 0;
    public float multiplier = 1;
    override public void Perform(RelicEventArgs eventArgs) {
        float healing = 0;
        if (valueType == RelicValueType.Static) {
            healing = amount;
        } else if (valueType == RelicValueType.Source) {
            healing = eventArgs.FloatValue;
        }

        CRUnit unit = null;
        if (target == RelicTarget.Owner) {
            unit = owner;
        } else if (target == RelicTarget.Initiator) {
            unit = eventArgs.Initiator;
        } else if (target == RelicTarget.Target) {
            unit = eventArgs.Target;
        }
        unit.heal(eventArgs.FloatValue * multiplier, relic.owner, 0);
    }
}