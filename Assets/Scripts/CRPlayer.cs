using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using DunGen;

public struct TargetDisplay {
    public targetType type;
    public targetModifiers modifiers;
    public float radius;
    public float pathRadius;
    public float range;
    public int degrees;
    public Vector2 location;
}

public class CRPlayer : CRUnit {
    private Vector2 m_Move;
    public PlayerInput playerInfo;
    [HideInInspector] public bool inMenu = false;    
    //private float blinkTimer = 0f;
    public bool assigningRune = false;
    [HideInInspector] public bool isBlinking = false;
    [SerializeField] public RadialMenu menu;
    [SerializeField] public RadialMenuScreen consumableMenu;
    [SerializeField] public RadialMenuScreen skillMenu;
    [SerializeField] public GameObject ChildWithAnimator;
    [SerializeField] public PlayerIndicator indicator;
    [SerializeField] private Transform Weapon;
    [SerializeField] public Transform oocTarget;
    [SerializeField] public Transform oocLinearTarget;
    [SerializeField] public GameObject oocProjectile;
    [SerializeField] public Interactable interactingItem = null;
    [SerializeField] public List<GameObject> startingSkills;
    [SerializeField] public SpriteRenderer minimapIcon;
    [HideInInspector] public List<GameObject> OwnedRelics = new List<GameObject>();
    [HideInInspector] public CharacterHUD hud;
    private Vector2 lastDir = Vector2.zero;
    [HideInInspector] public TreasureOverlay InteractingTreasure = null;

    public override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        ani = ChildWithAnimator.GetComponent<Animator>();
        playerInfo = GetComponent<PlayerInput>();
        unitColor = GameConfig.PlayerColors[playerInfo.playerIndex];
        indicator.SetColor(unitColor);
        minimapIcon.color = unitColor;
        
        game.RegisterPlayer(this);

        var character = GetComponent<DungenCharacter>();  	
        character.OnTileChanged += OnCharacterTileChanged; 

        // Setup key bindings
        GetComponent<PlayerInput>().onActionTriggered += HandleAction;
    }

    private void FixedUpdate() {
        UpdateAnimation();
    }

    override public void Update() {
        base.Update();

        m_Move = Vector2.zero;
        if (!inMenu && !inCombat && !isTargeting) {
            m_Move = lastDir;
            if (m_Move == Vector2.zero) {
                WorldMoveStop();
            } else {
                isMoving = true;
                indicator.SetPointerActive(true);
            }
        }
        
        if (isTargeting) {
            m_Move = lastDir;
            if (targetDisplay.type == targetType.Linear) {
                oocLinearTarget.GetComponent<oocTargeter>().Move(m_Move, targetDisplay);
            } else {
                oocTarget.GetComponent<oocTargeter>().Move(m_Move, targetDisplay);
            }
        } else {
            rb.velocity = new Vector2(m_Move.x, m_Move.y) * MoveSpeed;
        }
        ani.SetFloat("speed", MoveSpeed/15f * 2f);

        if (inCombat) {
            if (game.AI.CanEscapeCombat(this)) {
                ExitCombat();
            }
        }

        if (inMenu && isCombatMoving) {
            HideRadialMenuScreen();
        }
    }

    public void OnWorldMove(InputAction.CallbackContext ctx) {
        if (isStunned > 0 || isDead) {
            lastDir = Vector2.zero;
            return;
        }

        if (ctx.performed) { 
            lastDir = ctx.ReadValue<Vector2>();
        }

        if (ctx.canceled) {
            lastDir = Vector2.zero;
        }

        //if (inMenu) { menu.OnRadialMove(ctx); }
        else { indicator.OnInput(ctx); }
    }

    public void UpdateAnimation() {
        AnimationState newState = AnimationState.idle;

        if (isDead) {
            newState = AnimationState.dead;
        } else if (isAttacking) {
            newState = AnimationState.attack;
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
                isAttacking = false;
            }
        } else if (isMoving) {
            float x,y;
            if (Mathf.Abs(m_Move.x) > Mathf.Abs(m_Move.y)) {
                x = m_Move.x; y = 0f;
            } else {
                y = m_Move.y; x = 0f;
            }
            newState = AnimationState.move;
            int newDirection = (int)Util.SnapTo(Util.Angle(m_Move), 90) / 90 % 4;
            if (newDirection != ani.GetInteger("direction")) {
                ani.SetInteger("direction", newDirection);
            }
        } else if (isCasting) {
            newState = AnimationState.cast;
        } else {
            newState = AnimationState.idle;
        }

        if ((int)newState != ani.GetInteger("state")) {
            ani.SetInteger("state", (int)newState);
        }

        /*
        //blink sometimes
        blinkTimer += Time.deltaTime;
        if (!ani.GetBool("isMoving") && blinkTimer >= 3f) {
            blinkTimer = 0f;
            isBlinking = true;
        }
        ani.SetBool("isBlinking", isBlinking);
        */
    }

    private void HandleAction(InputAction.CallbackContext ctx) {
        var actions = new Dictionary<string, Action>() {
            // Player Actions
            { "Move",           () => OnWorldMove(ctx) },
            { "CombatMove",     () => CombatMove(ctx) },
            { "Affirm",         () => OnAffirmButton(ctx) },
            { "Cancel",         () => OnCancelButton(ctx) },
            { "OpenRadialMenu", () => OnRadialMenuButton(ctx) },

            // Radial Menu
            { "MenuMove",       () => menu.OnRadialMove(ctx) },
            { "MenuAffirm",     () => menu.OnRadialSelect(ctx) },
            { "MenuCancel",     () => menu.OnRadialCancel(ctx) },

            // Treasure
            { "TreasureMove",   () => TreasureMove(ctx) },
            { "TreasureAffirm", () => TreasureAffirm(ctx) },
            { "TreasureCancel", () => TreasureCancel(ctx) }
        };

        Action action = null;
        actions.TryGetValue(ctx.action.name, out action);
        if (action != null) action();
    }

    public void WorldMoveStop() {
        m_Move = Vector2.zero;
        isMoving = false;
        indicator.SetPointerActive(false);
    }

    public void OnRespawn(InputAction.CallbackContext ctx) {
        transform.position = new Vector3(0, 0, 0);
    }

    public void OnAffirmButton(InputAction.CallbackContext ctx) {
        /*interactingItem = null;
        foreach (Interactable i in FindObjectsOfType<Interactable>()) {
            if (i.interactingPlayer == this) {
                interactingItem = i;
            }
        }*/
        if (isStunned > 0 || isDead) return;

        if (playerInfo && !inMenu && ctx.started) {
            if (isCombatMoving) {
                endMove();
            } else if (inCombat && !isTargeting && remainingActions > 0 && game.playerTurn) {
                StartBasicAttack();
            } else if (inCombat && isTargeting) {
                ExecuteAttack();
            } else if (!inCombat && isTargeting) {
                if (targetDisplay.type == targetType.Direct) {
                    oocTarget.GetComponent<oocTargeter>().Execute(targetSkill);
                } else if (targetDisplay.type == targetType.Linear) {
                    oocLinearTarget.GetComponent<oocTargeter>().Execute(targetSkill);
                }
                stopTargeting();
            } else if (!inCombat && interactingItem == null) {
                isAttacking = true;
            } else if (!inCombat && interactingItem != null && !assigningRune) {
                interactingItem.OnInteract(this);
            }
        } else if (playerInfo && inMenu) {
            //menu.OnRadialSelect(ctx);
        }
    }

    public void OnRadialMenuButton(InputAction.CallbackContext ctx) {
        if (playerInfo == null || assigningRune || isStunned > 0 || isDead) return;
        if (ctx.started && !isCombatMoving && !isTargeting) {
            ShowRadialMenu();
            EnableRadialMenuActionMap(true);
        } else if (ctx.canceled) {
            CloseRadialMenu();
            EnableRadialMenuActionMap(false);
            playerInfo.actions.FindActionMap("Player").Enable();
        }
    }

    public void OnCancelButton(InputAction.CallbackContext ctx) {
        if (playerInfo == null) return;
        if (inMenu) {
            //menu.OnRadialCancel(ctx);
            return;
        }
        if (ctx.started) {
            CancelAction(false);
        }
    }

    public void EnterCombat() {
        if (!inCombat) {
            WorldMoveStop();
            inCombat = true;
            remainingActions = BaseActions;
            Vector3 cellPos = grid.WorldToCell(collisionPoint);
            transform.position = cellPos + UnitGridOffset;
            indicator.gameObject.SetActive(false);
        }
    }

    public void ExitCombat() {
        inCombat = false;
        if (isCombatMoving) {
            endMove();
        }
        indicator.gameObject.SetActive(true);
    }

    public void CombatMove(InputAction.CallbackContext ctx) {
        if (inCombat && !inMenu && ctx.performed && remainingMove >= 1 && !isTargeting) {
            Vector2 moveVal = ctx.ReadValue<Vector2>();
            float normal = Mathf.Max(Mathf.Abs(moveVal.x), Mathf.Abs(moveVal.y));

            Vector3Int moveInt = new Vector3Int(Mathf.RoundToInt(moveVal.x/normal), Mathf.RoundToInt(moveVal.y/normal), 0);

            moveTo(moveInt);

            ani.SetInteger("direction", (int)Util.SnapTo(Util.Angle(moveVal), 90) / 90 % 4);
        } else if (inCombat && isTargeting && ctx.performed) {
            Vector2 moveVal = ctx.ReadValue<Vector2>();
            float normal = Mathf.Max(Mathf.Abs(moveVal.x), Mathf.Abs(moveVal.y));

            Vector3Int moveInt = new Vector3Int(Mathf.RoundToInt(moveVal.x/normal), Mathf.RoundToInt(moveVal.y/normal), 0);
            selection.Move(moveInt);
        }
    }

    public void CancelAction(bool forceCancel = false) {
        if (isCombatMoving) {
            endMove(true);
        }

        if (isTargeting) {
            if (targetSkill) {
                targetSkill.cancel();
            }
            isCasting = false;
            stopTargeting();
        }

        if (inMenu && forceCancel) {
            HideRadialMenuScreen();
        }
    }

    override public void stopTargeting() {
        if (inCombat) {
            base.stopTargeting();
        } else {
            if (targetDisplay.type == targetType.Linear) {
                oocLinearTarget.gameObject.SetActive(false);
            } else {
                oocTarget.gameObject.SetActive(false); 
            }
            isTargeting = false;
        }
    }

    public void ShowRadialMenu() {
        menu.ShowScreen();
        inMenu = true;
        WorldMoveStop();
        indicator.gameObject.SetActive(false);
    }

    public void HideRadialMenuScreen() {
        if (menu.HideScreen()) {
            inMenu = false;
            if (!inCombat) {
                m_Move = lastDir;
            }
            if (!inCombat) {
                indicator.gameObject.SetActive(true);
            }
        }
    }

    public void CloseRadialMenu() {
        menu.HideScreen();
        inMenu = false; 
        if (!inCombat) {
            m_Move = lastDir;
        }
        if (!inCombat) {
            indicator.gameObject.SetActive(true);
        }
    }

    override public void startTargeting(targetType type = targetType.None, targetModifiers modifiers = targetModifiers.None, float range = 1f, float radius = 0f, float pathRadius = 0f, int degrees = 1) {
        base.startTargeting(type, modifiers, range, radius, pathRadius, degrees); 
    }

    override public void startTargeting(TargetDisplay td) {
        CloseRadialMenu();
        if (inCombat) {
            base.startTargeting(td);
        } else {
            targetDisplay = td;

            Vector3 coord = transform.position;
            targetDisplay.location = new Vector2(coord.x, coord.y);

            if (td.type == targetType.Direct) {
                oocTarget.gameObject.SetActive(true);
                oocTarget.GetComponent<oocTargeter>().Initialize(td);
            } else if (td.type == targetType.Linear) {
                oocLinearTarget.gameObject.SetActive(true);
                oocLinearTarget.GetComponent<oocTargeter>().Initialize(td);
            }

            isTargeting = true;
            isCasting = true;
        }
    }

    public override void SetStunned(int newStunnedValue) {
        base.SetStunned(newStunnedValue);
        if (isStunned > 0) {
            WorldMoveStop();
            CancelAction();
        }
    }

    override protected void onDeath() {
        base.onDeath();
    }

    //public void startLinearTargeting(float range)

    private void OnCharacterTileChanged(DungenCharacter character, Tile previousTile, Tile newTile) {
        FogOfWar fow = newTile.GetComponentInChildren<FogOfWar>();
        if (fow) {
            fow.RevealRoom(transform);
        }
    }

    public void AddRelic(GameObject relic) {
        GameObject relicInstance = Instantiate(relic, Vector3.zero, Quaternion.identity, transform);
        OwnedRelics.Add(relicInstance);
        Relic r = relicInstance.GetComponent<Relic>();
        r.owner = this;
        hud.UpdateRelics();
    }

    public void TreasureMove(InputAction.CallbackContext ctx) {
        if (InteractingTreasure) {
            InteractingTreasure.TreasureMove(ctx, this);
        }
    }

    public void TreasureAffirm(InputAction.CallbackContext ctx) {
       if (InteractingTreasure) {
            InteractingTreasure.TreasureAffirm(ctx, this);
        }
    }

    public void TreasureCancel(InputAction.CallbackContext ctx) {
        if (InteractingTreasure) {
            InteractingTreasure.TreasureCancel(ctx, this);
        }
    }

    public void EnableRadialMenuActionMap(bool b) {
        if (b) {
            playerInfo.actions.FindActionMap("RadialMenu").Enable();
        } else {
            playerInfo.actions.FindActionMap("RadialMenu").Disable();
        }
    }
}
