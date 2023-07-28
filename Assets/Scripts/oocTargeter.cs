using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oocTargeter : MonoBehaviour {
    public CRPlayer player;
    public List<CRUnit> targets;

    public virtual void OnTriggerEnter2D(Collider2D other) {
        CRUnit unit = other.GetComponent<CRUnit>();
        bool cantTargetCaster = (player.targetDisplay.modifiers & targetModifiers.CantTargetCaster) > 0;

        if (unit) {
            if (player == unit && cantTargetCaster) {
                return;
            }
            targets.Add(unit);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other) {
        CRUnit unit = other.GetComponent<CRUnit>();
        if (unit && targets.Contains(unit)) {
            targets.Remove(unit);
        }
    }

    public virtual void Initialize(TargetDisplay td) {
    }

    public virtual void Move(Vector3 move, TargetDisplay targetDisplay) {

    }

    public virtual void Execute(Skill skill) {

    }
}
