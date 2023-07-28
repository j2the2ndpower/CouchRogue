using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DunGen;

public enum AIState {
    idle = 0,
    move = 1,
    attack = 2,
    none = 3
}

public class CREnemy : CRUnit {
    [SerializeField] public bool SpawnsInPacks;
    [SerializeField] public ColliderRange alertCol;
    [SerializeField] public ColliderRange chaseCol;
    private Dictionary<int, CRPlayer> playersInAlert = new Dictionary<int, CRPlayer>();
    private Dictionary<int, CRPlayer> playersInChase = new Dictionary<int, CRPlayer>();
    private Dictionary<int, CRPlayer> playersSeen = new Dictionary<int, CRPlayer>();
    public bool turnActive;
    public bool canAttack = false;
    public AIState aiState = AIState.idle;
    public AIState goalState = AIState.idle;
    public AIState pendingAIState = AIState.none;
    public Skill aiStateSkill;

    public List<GameObject> lootTablePrefabs;
    public List<float> lootTableChances;
    [HideInInspector] public List<Skill> skills;
    private Tile tile;

    public override void Start() {
        base.Start();
        transform.position += UnitGridOffset;
        unitColor = GameConfig.enemyColor;

        var character = GetComponent<DungenCharacter>();  	
        character.OnTileChanged += OnCharacterTileChanged; 

        //Add skills to list
        foreach(Skill skill in GetComponentsInChildren<Skill>()) {
            skills.Add(skill);
            skill.SetCaster(this);
        }
    }

    override public void Update() {
        base.Update();
        if (isDead) { return; }

        if (inCombat) {
            //Force players into combat if they are too close
            CRPlayer[] players = new CRPlayer[playersInChase.Values.Count];
            playersInChase.Values.CopyTo(players, 0);
            foreach(CRPlayer player in players) {
                if (player.isDead) {
                    int playerIndex = player.playerInfo.playerIndex;
                    if (playersInAlert.ContainsKey(playerIndex)) {
                        playersInAlert.Remove(playerIndex);
                    } 
                    if (playersInChase.ContainsKey(playerIndex)) {
                        playersInChase.Remove(playerIndex);
                    } 
                } else {
                    player.EnterCombat();
                }
            }

            if (playersInChase.Count == 0) {
                inCombat = false;
                playersSeen.Clear();
            }
        }

        if (!inCombat && playersInAlert.Count > 0) {
            inCombat = true;
            game.StartCombat();

            // if you're in combat with another room, show that room
            if (tile) {
                FogOfWar fow = tile.GetComponentInChildren<FogOfWar>();
                if (fow && fow.isActiveAndEnabled) {
                    fow.RevealRoom(playersInAlert[0].transform);
                }
            }
        }
    }

    public void CombatMove(Vector3Int move) {
        transform.position += move;
    }

    public bool PlayerCanEscape(CRPlayer player) {
        int playerIndex = player.GetComponent<PlayerInput>().playerIndex;
        return !playersInChase.ContainsKey(playerIndex);
    }

    public void OnEnterRange(ColliderRange range, Collider2D col) {
        CRPlayer player = col.gameObject.GetComponent<CRPlayer>();
        if (player && !player.isDead) {
            int playerIndex = player.GetComponent<PlayerInput>().playerIndex;
            player.collisionPoint = col.ClosestPoint(transform.position);
            if (range == this.alertCol && !playersInAlert.ContainsKey(playerIndex)) {
                playersInAlert.Add(playerIndex, player);
            }
            if (range == chaseCol && !playersInChase.ContainsKey(playerIndex)) {
                playersInChase.Add(playerIndex, player);
            }
            if (!playersSeen.ContainsKey(playerIndex)) {
                playersSeen.Add(playerIndex, player);
            }
        }
    }

    public void OnExitRange(ColliderRange range, Collider2D col) {
        CRPlayer player = col.gameObject.GetComponent<CRPlayer>();
        if (player) {
            int playerIndex = player.GetComponent<PlayerInput>().playerIndex;
            if (range == alertCol && playersInAlert.ContainsKey(playerIndex)) {
                playersInAlert.Remove(playerIndex);
            }
            if (range == chaseCol && playersInChase.ContainsKey(playerIndex)) {
                playersInChase.Remove(playerIndex);
            }
        }
    }

    override protected void onDeath() {
        base.onDeath();
        game.AI.RemoveEnemy(this);
        spriteRenderer.color = Color.clear;
        Invoke("DestroyObject", 1.5f);
        for (int i = 0; i < lootTablePrefabs.Count; i++) {
            if (Random.Range(0f, 1f) < (lootTableChances[i] / 100))
            Instantiate(lootTablePrefabs[i], transform.position - UnitGridOffset, Quaternion.identity);
        }
    }

    public override void SetStunned(int newStunnedValue) {
        base.SetStunned(newStunnedValue);
        if (isStunned > 0) {
            stopTargeting();
        }
    }
    
    public void DestroyObject() {
        Destroy(gameObject);
    }

    public void SetAIState(AIState aiState, AIState goalState) {
        this.aiState = aiState;
        this.goalState = goalState;
        this.pendingAIState = aiState;
    }

    private void OnCharacterTileChanged(DungenCharacter character, Tile previousTile, Tile newTile) {
        tile = newTile;
    }
}
