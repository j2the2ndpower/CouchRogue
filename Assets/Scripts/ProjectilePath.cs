using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePath : MonoBehaviour {
    public Projectile projectile;

    private void Start() {
        projectile = transform.parent.GetComponent<Projectile>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CRUnit target = other.GetComponent<CRUnit>();
        if (target && target != projectile.player) {
            if ((projectile.targetDisplay.modifiers & targetModifiers.Piercing) > 0) {
                projectile.Detonate(target);
            } else {
                projectile.Detonate();
            }
        }
    }
}
