using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class LayerSorting : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        spriteRenderer.sortingOrder = (int)(transform.position.y*-10);
    }
}
