using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTargets : SkillInstruction {
    public float amount;
    override public void Cast(List<CRUnit> targets) {
        float healing = amount+skill.skillEffectiveness;
        foreach (CRUnit target in targets) {
            target.heal(healing, caster, skill.increasedCritChance);
            for (int i = 0; i < skill.GetKeywordCount(SkillKeyword.Rectify); i++) {
                if (!target.IsFriendly(caster)) {
                    target.takeDamage(healing, caster, skill.increasedCritChance);
                }
            }
        }
    }
}
