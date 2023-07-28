using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleText {
    public string text;
    public Color color;
    public float timeAlive;
    public GameObject gameObject;
    public bool started;
    public bool crit;
    public bool fake;
    
    public BattleText(string text, Color color, bool crit, bool fake) {
        this.text = text;
        this.color = color;
        this.crit = crit;
        this.timeAlive = 0f;
        this.started = false;
        this.gameObject = null;
        this.fake = fake;
    }
}

public class ScrollingBattleText : MonoBehaviour {
    public GameObject TextObjectPrefab;
    public float velocity = 3f;
    public float duration = 1f;
    public float spacing = 0.5f;

    private List<BattleText> textQueue = new List<BattleText>();
    private BattleText lastBt = null;

    public void emit(string text, Color color, bool crit) {
        BattleText bt = new BattleText(text, color, crit, false);
        textQueue.Add(bt);
    }

    private void Update() {
        if (textQueue.Count == 0) return;

        for (int i = 0; i < textQueue.Count; i++) {
            BattleText bt = textQueue[i];
            if ((!bt.started && lastBt == null) || (!bt.started && lastBt.gameObject && (velocity * lastBt.timeAlive) >= spacing)) {
                CreateTextObject(i);
                lastBt = bt;
            } else if (bt.started) {
                bt.gameObject.transform.Translate(Vector3.up * velocity * Time.deltaTime);
                bt.timeAlive = bt.timeAlive + Time.deltaTime;
            }
        }

        for (int i = textQueue.Count-1; i >= 0; i--) {
            BattleText bt = textQueue[i];
            if (bt.started && bt.timeAlive >= duration) {
                textQueue.RemoveAt(i);
                Destroy(bt.gameObject);
            }
        }

        if (textQueue.Count == 0) {
            lastBt = null;
        }

    }

    private GameObject CreateTextObject(int battleTextIndex) {
        BattleText battleText = textQueue[battleTextIndex];
        GameObject newTextGO = Instantiate(TextObjectPrefab, this.transform);
        newTextGO.transform.SetParent(null);
        TextMeshPro tmp = newTextGO.GetComponent<TextMeshPro>();
        tmp.color = battleText.color;
        tmp.text = battleText.text;
        if (battleText.crit) {
            tmp.fontSize += 2;
            tmp.outlineColor = Color.yellow;
        }
        textQueue[battleTextIndex].gameObject = newTextGO;
        textQueue[battleTextIndex].started = true;

        return newTextGO;
    }
}
