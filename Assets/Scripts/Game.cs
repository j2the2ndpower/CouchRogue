using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum statType {
    None,
    Health,
    Mana,
    Stamina,
    Rage,
    Tenacity
};

public enum targetType {
    None,
    Melee,
    Linear,
    Direct,
    Cone,
    Self
};

[Flags] public enum targetModifiers {
    None = 0,
    Piercing = 1,
    Exploding = 2,
    CantTargetCaster = 4
};

public class Game : MonoBehaviour {
    [SerializeField] public CombatTimer timer;
    [SerializeField] public AICollective AI;
    [SerializeField] public float tickDuration;
    public float tickTimer = 0f;
    public CRPlayer[] players = new CRPlayer[0];
    public GameObject CharacterHUDPrefab;
    private MultiplayerZoomCamera m_Camera;
    private Canvas m_Canvas;
    public bool inCombat = false;
    public bool playerTurn = true;
    public GlobalEffects gEffects;

    public List<GameObject> allStatusEffects = new List<GameObject>();
    public List<GameObject> negativeStatusEffects = new List<GameObject>();
    public List<GameObject> positiveStatusEffects = new List<GameObject>();

    public void Start() {
        m_Camera = FindObjectOfType<MultiplayerZoomCamera>();
        m_Canvas = FindObjectOfType<Canvas>();

        GameObject[] statusEffects = Resources.LoadAll<GameObject>("StatusEffects");
        foreach (GameObject statusEffect in statusEffects) {
            allStatusEffects.Add(statusEffect);
            if ( statusEffect.GetComponent<StatusEffect>().negative ) {
                negativeStatusEffects.Add(statusEffect);
            } else {
                positiveStatusEffects.Add(statusEffect);
            }
        }
        Debug.Log("Loaded " + allStatusEffects.Count + " status effects.");
        Debug.Log("Loaded " + negativeStatusEffects.Count + " negative status effects.");
        Debug.Log("Loaded " + positiveStatusEffects.Count + " positive status effects.");
    }

    public void Update() {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickDuration) {
            UnitTick();
            tickTimer = 0;
        }
    }

    public void StartCombat() {
        if (inCombat) return;

        inCombat = true;
        //determine turn order
        playerTurn = true;

        StartTurn(playerTurn);
    }

    public void StartTurn(bool playerTurn) {
        if (playerTurn) {
            foreach (CRPlayer player in players) {
                player.remainingActions = player.BaseActions + player.heldActions;
                //TODO: ALTERNATIVE LOGIC
                /*player.remainingActions += player.BaseActions;
                player.remainingActions = Mathf.Clamp(player.remainingActions, 0, player.BaseActions);
                player.remainingActions = Mathf.Clamp(player.remainingActions + player.heldActions, 0, player.BaseActions + player.heldActions);*/
                player.heldActions = 0;
            }
            focusPlayers();
            timer.gameObject.SetActive(true);
            timer.StartTimer();
            foreach(CRPlayer player in players) {
                if (player.inCombat) player.OnTick(TickType.StartTurn);
            }
        } else {
            timer.gameObject.SetActive(false);
            AI.StartTurn();
        }
    }

    public void EndTurn() {
        if (playerTurn) {
            CancelAllPlayerAction();
        } else {
            AI.EndTurn();
        }
        playerTurn = !playerTurn;
        StartTurn(playerTurn);
    }

    public void EndCombat() {
        CancelAllPlayerAction();
        inCombat = false;
        timer.gameObject.SetActive(false);
        focusPlayers();
        AI.EndCombat();
    }

    public void CancelAllPlayerAction() {
        foreach (CRPlayer player in players) {
            player.CancelAction(true);
            if (player.inCombat) player.OnTick(TickType.EndTurn);
        }
    }

    public void focusPlayers() {
        m_Camera.ClearTargets();
        foreach (CRPlayer player in players) {
            if (!player.isDead) {
                m_Camera.AddTarget(player.transform);
            }
        }
    }

    public void RegisterPlayer(CRPlayer player) {
        Array.Resize<CRPlayer>(ref players, players.Length+1);
        players.SetValue(player, player.GetComponent<PlayerInput>().playerIndex);
        m_Camera.AddTarget(player.transform);
        CharacterHUD hud = Instantiate(CharacterHUDPrefab, m_Canvas.transform.Find("PortraitLayout")).GetComponent<CharacterHUD>();
        player.hud = hud;
        hud.SetPlayer(player, player.GetComponent<PlayerInput>().playerIndex);
    }

    public Color getStatColor(statType type) {
        Color returnColor;

        switch (type) {
            case (statType.Health):
                returnColor = new Color32(0x33, 0xCC, 0x33, 0xFF);
                break;
            case (statType.Mana):
                returnColor = new Color32(0x33, 0x66, 0xFF, 0xFF);
                break;
            case (statType.Rage):
                returnColor = new Color32(0xFF, 0x00, 0x00, 0xFF);
                break;
            case (statType.Stamina):
                returnColor = new Color32(0xFF, 0xCC, 0x33, 0xFF);
                break;
            case (statType.Tenacity):
                returnColor = new Color32(0xCC, 0x33, 0x99, 0xFF);
                break;
            default:
                returnColor = Color.white;
                break;
        }

        return returnColor;
    }


    public Color getNegativeStatColor(statType type) {
        Color returnColor;

        switch (type) {
            case (statType.Health):
                returnColor = new Color32(0xFF, 0x00, 0x00, 0xFF);
                break;
            case (statType.Mana):
                returnColor = new Color32(0x33, 0x66, 0xFF, 0xFF);
                break;
            case (statType.Rage):
                returnColor = new Color32(0x33, 0xCC, 0x33, 0xFF);
                break;
            case (statType.Stamina):
                returnColor = new Color32(0xFF, 0xCC, 0x33, 0xFF);
                break;
            case (statType.Tenacity):
                returnColor = new Color32(0xCC, 0x33, 0x99, 0xFF);
                break;
            default:
                returnColor = Color.white;
                break;
        }

        return returnColor;
    }

    public void UnitTick() {
        foreach(CRUnit unit in FindObjectsOfType<CRUnit>()) {
            if (!unit.inCombat) { 
                unit.OnTick(TickType.EndTurn);
                unit.OnTick(TickType.StartTurn); 
            }
        }
    }

    public static CRUnit[] GetUnits() {
        return FindObjectsOfType<CRUnit>();
    }

    public void OpenTreasureForPlayers(bool b, TreasureOverlay treasure) {
        for(int i = 0; i < players.Length; i++) {
            if (!players[i].assigningRune) { players[i].inMenu = b; }
            players[i].InteractingTreasure = treasure;
            if (b) {
                players[i].playerInfo.actions.FindActionMap("Treasure").Enable();
            } else {
                players[i].playerInfo.actions.FindActionMap("Treasure").Disable();
            }
        }
    }
}
