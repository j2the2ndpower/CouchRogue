using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oocLinearTargeter : oocTargeter {
    public Transform pathRadiusBase;
    public Transform rotater;
    public Transform pathRadiusLine;
    public Transform pathRadiusTarget;
    public Transform radiusTarget;
    public Transform radiusTargetBg;

    private float radius;
    private float pathRadius;
    
    
    public override void Initialize(TargetDisplay td) {
        base.Initialize( td);
        radius = (td.radius) * 2;
        pathRadius = (td.pathRadius) * 2;
        radiusTarget.position = player.transform.position;
        pathRadiusTarget.position = player.transform.position;

        //Reset rotation
        rotater.localRotation = Quaternion.AngleAxis(-Util.Angle(Vector2.zero), Vector3.forward);

        //Radius
        radiusTarget.localScale = new Vector3(radius, radius);
        radiusTarget.position = pathRadiusBase.position + new Vector3(0f, td.range);
        pathRadiusTarget.position = radiusTarget.position;

        //Path Radius
        pathRadiusBase.localScale = new Vector3(pathRadius, pathRadius);
        pathRadiusLine.localScale = new Vector3(pathRadius, Vector3.Distance(radiusTarget.position, pathRadiusLine.position));
        pathRadiusTarget.localScale = new Vector3(pathRadius, pathRadius);

        //Color
        pathRadiusBase.GetComponent<SpriteRenderer>().color = player.unitColor;
        pathRadiusLine.GetComponent<SpriteRenderer>().color = player.unitColor;
        pathRadiusTarget.GetComponent<SpriteRenderer>().color = player.unitColor;
        radiusTarget.GetComponent<SpriteRenderer>().color = player.unitColor;
        radiusTargetBg.GetComponent<SpriteRenderer>().color = player.unitColor;
    }

    public override void Move(Vector3 move, TargetDisplay targetDisplay) {
        base.Move(move, targetDisplay);
        rotater.localRotation = Quaternion.AngleAxis(-Util.Angle(move), Vector3.forward);

/*        radiusTarget.GetComponent<Rigidbody2D>().velocity = new Vector2(move.x, move.y) * player.MoveSpeed;
        float distance = Vector2.Distance(radiusTarget.position, player.transform.position);
        if (distance > targetDisplay.range) {
            Vector3 fromOriginToObject = radiusTarget.position - player.transform.position;
            fromOriginToObject *= targetDisplay.range / distance; //Magic line
            radiusTarget.position = player.transform.position + fromOriginToObject;
        }
        pathRadiusTarget.position = radiusTarget.position;

        Vector3 d = pathRadiusTarget.position - pathRadiusBase.position;
        pathRadiusLine.localScale = new Vector3(pathRadius, Vector3.Distance(radiusTarget.position, pathRadiusLine.position));
        rotater.localRotation = Quaternion.AngleAxis(-Util.Angle(d), Vector3.forward);*/
    }

    public override void Execute(Skill skill) {
        GameObject pObject = Instantiate(player.oocProjectile, transform.position, Quaternion.identity);
        Projectile p = pObject.GetComponent<Projectile>();
        p.skill = skill;
        p.direction = rotater.up*player.MoveSpeed;
        p.player = player;
        p.targetDisplay = player.targetDisplay;
        pObject.GetComponent<oocTargeter>().player = player;
        pObject.GetComponent<CircleCollider2D>().radius = radius;
        p.pathProjectile.GetComponent<CircleCollider2D>().radius = pathRadius;
        GameObject eObject = GameObject.Instantiate(skill.effect, pObject.transform);
        AfterEffect e = eObject.GetComponent<AfterEffect>();
        e.dontDestroy = true;
        p.effect = e;
        skill.hideEffect = true;
    }
}
