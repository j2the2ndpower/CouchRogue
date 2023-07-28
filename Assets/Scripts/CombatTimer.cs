using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTimer : MonoBehaviour {
    [SerializeField] public float turnTime;
    [SerializeField] public Image timer;
    private float timeRemaining;
    private float width;
    private Game game;

    private void Start() {
        game = FindObjectOfType<Game>();
        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();
        RectTransform rect = GetComponent<RectTransform>();
        width =  parentRect.sizeDelta.x + rect.sizeDelta.x;
    }

    void Update() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            //End Turn
            game.EndTurn();
        }
        timer.rectTransform.sizeDelta = new Vector2(timeRemaining/turnTime * width, timer.rectTransform.sizeDelta.y);

        bool endTurnEarly = true;
        foreach (CRPlayer player in game.players) {
            if (player.inCombat && player.isStunned == 0 && !player.isDead && (player.remainingActions > 0 || player.isCasting)) {
                endTurnEarly = false;
            }
        }

        if (endTurnEarly) {
            game.EndTurn();
        }
    }

    public void StartTimer() {
        timeRemaining = turnTime;
    }
}
