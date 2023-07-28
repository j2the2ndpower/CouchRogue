using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public float interactDistance; //The farthest a player can be to interact with the object
    public GameObject prompt; //Reference to the popup window

    [HideInInspector] public Game game;
    private RectTransform CanvasRect;
    private Vector2 ViewportPosition;
    [HideInInspector] public CRPlayer interactingPlayer;
    public Transform spriteTransform;
    public bool promptHidden = false;

    virtual public void Start() {
        game = GameObject.Find("Game").GetComponent<Game>();
        if (!spriteTransform) {
            spriteTransform = transform;
        }
    }

    void Update() {
        //Interacting Player walked too far away
        if (interactingPlayer) {
            float pdistance = Vector2.Distance(spriteTransform.position, interactingPlayer.transform.position);
            if (pdistance > interactDistance || interactingPlayer.interactingItem != this || (interactingPlayer.inMenu && !interactingPlayer.assigningRune) || interactingPlayer.isDead) {
                if (interactingPlayer.interactingItem == this) {
                    interactingPlayer.interactingItem = null;
                }
                interactingPlayer = null;
            }
        }

        //Check for a new interacting player
        if (!interactingPlayer) {
            foreach (CRPlayer p in game.players) {
                float pdistance = Vector2.Distance(spriteTransform.position, p.transform.position);
                if (
                  pdistance <= interactDistance && !p.inCombat && !p.inMenu && !p.isDead
                  && (
                    !p.interactingItem || 
                    Vector3.Distance(p.interactingItem.spriteTransform.position, p.transform.position) > pdistance
                  )
                ) {
                    if (p.interactingItem) {
                        p.interactingItem.interactingPlayer = null;
                    }
                    interactingPlayer = p;
                    p.interactingItem = this;
                }
            }
        }

        prompt.SetActive(interactingPlayer != null && !promptHidden);
    }

    virtual public void OnInteract(CRPlayer player) {
        
    }

    
    public void ShowPrompt() {
        promptHidden = false;
    }

    public void HidePrompt() {
        promptHidden = true;
    }
}
