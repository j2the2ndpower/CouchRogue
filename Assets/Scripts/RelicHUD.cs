using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicHUD : MonoBehaviour {
    [HideInInspector] public Image image;
    private Sprite relicSprite;

    void Start() {
        image = GetComponent<Image>();
        image.sprite = relicSprite;
    }

    public void SetSprite(Sprite sprite) {
        relicSprite = sprite;
        if (image) {
            image.sprite = relicSprite;
        }
    }
}
