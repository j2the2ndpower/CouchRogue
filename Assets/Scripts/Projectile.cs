using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Skill skill;
    public Vector3 direction;
    private Vector3 distanceTraveled;
    public CRUnit player;
    public Transform pathProjectile;
    public AfterEffect effect;
    public TargetDisplay targetDisplay;
    
    public void Detonate(CRUnit target = null) {
        if (target != null) {
            List<CRUnit> singleTarget = new List<CRUnit>();
            singleTarget.Add(target);
            skill.cast(singleTarget);
        } else {
            skill.cast(GetComponent<oocTargeter>().targets);
        }

        effect.dontDestroy = false;
        effect.elapsedTime = 0f;

        if ((targetDisplay.modifiers & targetModifiers.Piercing) == 0) { 
            print("not piercing");
            Destroy(gameObject);
            effect.transform.SetParent(null);
        }
    }

    private void Update() {
        Vector3 distance = direction*Time.deltaTime;
        transform.Translate(distance);
        distanceTraveled += distance;
        if (distanceTraveled.magnitude >= skill.range) {
            Detonate();
        }
    }
}
