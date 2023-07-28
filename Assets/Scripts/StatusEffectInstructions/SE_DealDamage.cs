using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageTarget {
    Self = 0,
    Attacker = 1,
}

public class SE_DealDamage : StatusEffectInstruction {
    public DamageTarget damageTarget;
    public float damage;
    public bool reflectiveDamage;

    override public void OnEvent(effectParamaters param) {
        CRUnit target = owner;
        if (reflectiveDamage && target == param.attacker) return;
        float dealtDamage = damage*statusEffect.magnitude;
        if (damageTarget == DamageTarget.Self) target = owner;
        if (damageTarget == DamageTarget.Attacker) { 
            target = param.attacker;
            if (reflectiveDamage) dealtDamage = Mathf.Clamp(dealtDamage, 0, param.finalDamage+damage);
        }
        target.takeDamage(dealtDamage, statusEffect.source, 0f);
    }
}
