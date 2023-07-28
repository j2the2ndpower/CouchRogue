using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSlot : MonoBehaviour
{
    public List<Sprite> runeSprites;
    public SpriteRenderer spriteRenderer;
    public CRPlayer player;
    
    public bool isPreview = false;

    public RuneType runeType = RuneType.None;

    private void Start() {
        SetRune(runeType);
        if (!isPreview) {
            hide();
        }
    }

    private void Update() {
        if (runeType == RuneType.None && player != null) {
            spriteRenderer.color = player.unitColor;
        } else {
            spriteRenderer.color = Color.white;
        }
    }

    public void SetRune(RuneType type) {
        runeType = type;
        this.spriteRenderer.sprite = runeSprites[(int)runeType];
    }

    public void hide() {
        spriteRenderer.gameObject.SetActive(false);
    }

    public void show() {
        spriteRenderer.gameObject.SetActive(true);
    }
}
