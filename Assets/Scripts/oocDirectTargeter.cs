using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oocDirectTargeter : oocTargeter {
    public override void Initialize(TargetDisplay td) {
        base.Initialize(td);
        transform.position = player.transform.position;
        float radiusScale = (td.radius) * 2;
        transform.localScale = new Vector3(radiusScale, radiusScale, 1);
        GetComponent<SpriteRenderer>().color = player.unitColor;
    }

    public override void Move(Vector3 move, TargetDisplay targetDisplay) {
        base.Move(move, targetDisplay);
        GetComponent<Rigidbody2D>().velocity = new Vector2(move.x, move.y) * player.MoveSpeed;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > targetDisplay.range) {
            Vector3 fromOriginToObject =  transform.position - player.transform.position;
            fromOriginToObject *= targetDisplay.range / distance; //Magic line
            transform.position = player.transform.position + fromOriginToObject;
        }
    }

    public override void Execute(Skill skill) {
        skill.cast(targets);
    }
}
