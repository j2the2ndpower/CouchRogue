using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneEffect : MonoBehaviour {
    public string description;
    [HideInInspector] public RuneInstruction[] instructions;
    [HideInInspector] public Skill skill;
    [HideInInspector] public CRUnit player;
    public RuneType type;

    private void Awake() {
        skill = transform.parent.GetComponent<Skill>();
        player = skill.caster;
        instructions = GetComponents<RuneInstruction>();
        for (int i = 0; i < instructions.Length; i++) {
            instructions[i].skill = skill;
            instructions[i].caster = player;
            instructions[i].runeEffect = this;
        }
    }

    public void Attach() {
        for (int i = 0; i < instructions.Length; i++) {
            instructions[i].Attach();
        }
    }

    public void OnCast(List<CRUnit> targets) {
        for (int i = 0; i < instructions.Length; i++) {
            instructions[i].OnCast(targets);
        }
    }
}
