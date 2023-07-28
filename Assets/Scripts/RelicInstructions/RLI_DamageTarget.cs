using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLI_DamageTarget : RelicInstruction {
    public RelicTarget target;
    public RelicValueType valueType;

    public float amount;
    public float multiplier = 1;

    override public void Perform(RelicEventArgs eventArgs) {
        CRUnit unit = null;
        float damage = 0;
        if (valueType == RelicValueType.Static) {
            damage = amount;
        } else if (valueType == RelicValueType.Source) {
            damage = eventArgs.FloatValue;
        }

        if (target == RelicTarget.Owner) {
            unit = owner;
        } else if (target == RelicTarget.Initiator) {
            unit = eventArgs.Initiator;
        } else if (target == RelicTarget.Target) {
            unit = eventArgs.Target;
        }
        unit.takeDamage(damage * multiplier, owner, 0);
    }
}