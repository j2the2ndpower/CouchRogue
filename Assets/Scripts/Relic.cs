using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour {
    public string Name;
    public string description;
    public Sprite icon;
    [HideInInspector] public RelicInstruction[] instructions;
    [HideInInspector] public CRUnit owner;
    
    public void Start() {
        instructions = GetComponents<RelicInstruction>();
        foreach(RelicInstruction instruction in instructions) {
            instruction.relic = this;
            instruction.owner = owner;
        }
    }
}
