using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillAttribute {
    Cost = 0,
    Radius = 1,
    Range = 2,
    TargetType = 3,
    SkillKeyword = 4,
    SkillEffectiveness = 5, 
    SkillCritChance = 6,
}

public class RI_ModifySkill : RuneInstruction {
    public SkillAttribute attribute;
    public ModifyOperation operation;
    public int intValue;
    public float floatValue;
    public targetType targetTypeValue;
    public SkillKeyword keywordValue;
    public statType costTypeValue;

    override public void Attach() {
        switch (attribute) {
            case (SkillAttribute.Cost): 
                skill.costDict[costTypeValue] = (int)Util.ApplyOperator(skill.costDict[costTypeValue], intValue, operation);
                break;
            case (SkillAttribute.Radius):
                skill.radius = Util.ApplyOperator(skill.radius, floatValue, operation);
                break;
            case (SkillAttribute.Range):
                skill.range = Util.ApplyOperator(skill.range, floatValue, operation);
                break;
            case (SkillAttribute.TargetType):
                skill.type = targetTypeValue;
                break;
            case (SkillAttribute.SkillKeyword):
                for (int i = 0; i < intValue; i++) {
                    skill.keywords.Add(keywordValue);
                }
                break;
            case (SkillAttribute.SkillEffectiveness):
                skill.skillEffectiveness = (int)Util.ApplyOperator(skill.skillEffectiveness, intValue, operation);
                break;
            case (SkillAttribute.SkillCritChance):
                skill.increasedCritChance = Util.ApplyOperator(skill.increasedCritChance, floatValue, operation);
                break;
            default: 
                break;
        }
        skill.UpdateTargetDisplay();
    }

    override public void OnCast(List<CRUnit> targets) {}
}
