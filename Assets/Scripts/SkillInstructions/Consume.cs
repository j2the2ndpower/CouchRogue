using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : SkillInstruction {
    override public void Cast(List<CRUnit> targets) {
        skill.Consume();
    }
}
