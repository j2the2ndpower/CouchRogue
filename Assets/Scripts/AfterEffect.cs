using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffect : MonoBehaviour {
    public CRUnit owner;
    public float duration;
    public bool dontDestroy = false;

    [HideInInspector] public float elapsedTime;

    private void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration && !dontDestroy) {
            DestroyEffect();
        }
    }

    public void DestroyEffect() {
        Destroy(gameObject);
    }
}
