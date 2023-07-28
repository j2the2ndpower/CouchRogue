using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTargets : SkillInstruction {
    public float amount;
    override public void Cast(List<CRUnit> targets) {
        float damage = amount+skill.skillEffectiveness;
        foreach (CRUnit target in targets) {
            target.takeDamage(damage, caster, skill.increasedCritChance);
        }
    }
}