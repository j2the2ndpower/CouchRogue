using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusEffectStack : MonoBehaviour {
    public Sprite sprite;
    public int count;
    public int stacks;

    [SerializeField] public TextMeshPro countText;
    [SerializeField] public TextMeshPro stackText;

    public void SetSprite(Sprite newSprite) {
        sprite = newSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetCount(int newCount) {
        count = newCount;
        countText.text = count.ToString();
    }

    public void SetStacks(int newStacks) {
        stacks = newStacks;
        stackText.text = stacks.ToString();
    }

    public void ShowStacks(bool show) {
        stackText.gameObject.SetActive(show);
    }
}
