using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class RuneInstruction : MonoBehaviour {
    [HideInInspector] public Skill skill;
    [HideInInspector] public CRUnit caster;
    [HideInInspector] public RuneEffect runeEffect;

    abstract public void Attach();

    abstract public void OnCast(List<CRUnit> targets);
}
