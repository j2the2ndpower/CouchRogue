using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SkillInstruction : MonoBehaviour {
    [HideInInspector] public Skill skill;
    [HideInInspector] public CRUnit caster;

    abstract public void Cast(List<CRUnit> targets);
}
