using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AnimationState {
    idle = 0,
    move = 1,
    attack = 2,
    cast = 3,
    dead = 4,
}

public struct modifyParamaters {
    public ModifyAttribute attribute;
    public ModifyOperation operation;
    public float floatValue;
    public int intValue;
    public bool boolValue;
}

abstract public class CRUnit : MonoBehaviour {
    [HideInInspector] public Grid grid;
    [HideInInspector] public Map map;
    [HideInInspector] public Game game;
    public Vector3 UnitGridOffset;
    public Vector3Int gridPosition;
    [HideInInspector] public bool inCombat = false;
    [HideInInspector] public float remainingMove;
    [HideInInspector] public Color unitColor;

    [SerializeField] public TextMeshPro moveText;
    [HideInInspector] public bool isCombatMoving = false;
    [HideInInspector] public int remainingActions = 0;
    [SerializeField] public GameObject selectionPrefab;
    public Animator AIAnimator;

    public TargetDisplay targetDisplay = new TargetDisplay();
    protected Rigidbody2D rb;
    protected Marker selectionMarker;
    protected Animator ani;
    protected SpriteRenderer spriteRenderer;
    [HideInInspector] public int heldActions = 0;

    [HideInInspector] public bool isAttacking = false;
    public bool isCasting = false;
    [HideInInspector] public bool isTargeting = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isBasicAttacking = false;
    [HideInInspector] public int isStunned = 0;
    [HideInInspector] public bool isDead = false;

    [HideInInspector] public Skill targetSkill;

    [SerializeField] public Vector3 collisionPoint;
    [SerializeField] public targetType BasicAttackType = targetType.Melee;
    [SerializeField] public float BasicAttackRange = 1f;
    [SerializeField] public float BasicAttackRad = 1f;
    [SerializeField] public float BasicAttackDamage = 5f;
    [SerializeField] public int MoveSpeed = 10;
    [SerializeField] public int BaseActions = 2;
    [SerializeField] public float CritChance = 10;
    [SerializeField] public float CritMultiplier = 1.5f;


    public SelectionMarker selection;

    [SerializeField] public statType[] stats;
    [SerializeField] public float[] baseStats;
    public Dictionary<statType, float> statValues = new Dictionary<statType, float>();
    public Dictionary<statType, float> maxStatValues = new Dictionary<statType, float>();
    public int armor = 0;
    public int strength = 0;
    public int shield = 0;
    public List<StatusEffect> statusEffects;

    public ScrollingBattleText scrollingBattleText;
    public Transform statusStackParent;
    public GameObject statusStackPrefab;
    public List<Marker> highlightedSpaces = new List<Marker>();
    public List<Marker> selectedSpaces = new List<Marker>();
    public Vector3Int anchorSpace;

    virtual public void Start() {
        grid = FindObjectOfType<Grid>();
        map = FindObjectOfType<Map>();
        game = FindObjectOfType<Game>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UnitGridOffset = new Vector3(grid.cellSize.x/2, grid.cellSize.y/4);

        //Create stats dictionary
        for (int i = 0; i < stats.Length; i++) {
            statValues.Add(stats[i], baseStats[i]);
            maxStatValues.Add(stats[i], baseStats[i]);
        }
    }

    virtual public void Update() {
        if (inCombat) {
            moveText.SetText(remainingMove.ToString());
        }

        UpdateGridPosition();
    }

    public void UpdateGridPosition() {
        Vector3Int currentGridPosition = grid.WorldToCell(transform.position);

        if (gridPosition != currentGridPosition) {
            map.SetSpaceOccupied(gridPosition, null);           
            gridPosition = currentGridPosition;
        }

        if (!map.IsSpaceOccupied(currentGridPosition)) {
            map.SetSpaceOccupied(currentGridPosition, this);
        }
    }

    public float getStat(statType type) {
        float returnValue;
        statValues.TryGetValue(type, out returnValue);
        return returnValue;
    }

    public float getMaxStat(statType type) {
        float returnValue;
        maxStatValues.TryGetValue(type, out returnValue);
        return returnValue;
    }

    public void takeDamage(float amount, CRUnit attacker, float bonusCrit) {
        if (isDead) return;
        //Check if The attack is a crit
        float critNum = Random.Range(0f, 100f);
        bool isCrit = critNum <= attacker.CritChance+bonusCrit;

        //calculate the damage
        float buffedDamage = amount + attacker.strength;
        if (isCrit) buffedDamage *= CritMultiplier;
        buffedDamage = Mathf.Floor(buffedDamage);

        //calculate the defense
        float finalDamage = Mathf.Floor(buffedDamage-armor);
        finalDamage -= reduceShield(finalDamage);
    
        //Reduce Health
        reduceStat(statType.Health, finalDamage, isCrit);

        //Call The Take Damage status effect event
        effectParamaters param = new effectParamaters();
        param.attacker = attacker;
        param.Damage = amount;
        param.buffedDamage = buffedDamage;
        param.finalDamage = finalDamage;
        OnStatusEvent(StatusEffectEvent.TakeDamage, param);

        //Die if you have no more health
        if (statValues[statType.Health] <= 0) {
            onDeath();
        }
    }

    public void heal(float amount, CRUnit healer, float bonusCrit, bool worksOnDead = false) {
        if (isDead && !worksOnDead) return;
        //Check if the heal is a crit
        float critNum = Random.Range(0f, 100f);
        bool isCrit = critNum <= healer.CritChance+bonusCrit;

        //Calculate the healing
        float healing = amount;
        if (isCrit) healing *= CritMultiplier;
        healing = Mathf.Floor(healing);

        //Increase Health
        increaseStat(statType.Health, healing, isCrit);

        //Fire `Player healed another Ally` event
        if (IsFriendly(healer) && healer != this) {
            RelicEventArgs e = new RelicEventArgs();
            e.Initiator = healer;
            e.Target = this;
            e.FloatValue = healing;
            e.EventType = RelicEventType.PlayerHealedAlly;
            RelicEvent.Fire(this, e);
        }

        //Instantiate Healing Particle effect
        GameObject ef = Instantiate(game.gEffects.effect["Heal"], transform);
        ef.GetComponent<AfterEffect>().owner = healer;
    }

    public void spendMana(float amount) {
        reduceStat(statType.Mana, amount, false);
    }

    public void spendResource(statType resource, float amount) {
        reduceStat(resource, amount, false);
    }

    public void reduceStat(statType type, float amount, bool isCrit) {
        if (statValues.ContainsKey(type)) {
            statValues[type] = Mathf.Clamp(statValues[type]-amount, 0, maxStatValues[type]);
            if (scrollingBattleText) {
                scrollingBattleText.emit((-amount).ToString(), game.getNegativeStatColor(type), isCrit);
            }
        }
    }

    public int reduceShield(float amount) {
        int previousShield = shield;
        shield = (int)Mathf.Clamp(shield-amount, 0, shield);
        return previousShield-shield;
    }

    public void increaseStat(statType type, float amount, bool isCrit) {
        if (statValues.ContainsKey(type)) {
            statValues[type] = Mathf.Clamp(statValues[type]+amount, 0, maxStatValues[type]);
            if (scrollingBattleText) {
                scrollingBattleText.emit(amount.ToString(), game.getStatColor(type), isCrit);
            }
        }
    }

    virtual protected void onDeath() {
        isDead = true;
        for (int i = statusEffects.Count-1; i >= 0; i--) {
            RemoveStatusEffect(statusEffects[i]);
        }
    }

    public void startMove() {
        if (grid == null || remainingActions == 0) { return; }

        isCombatMoving = true;
        moveText.gameObject.SetActive(true);
        remainingMove = MoveSpeed;
    }

    public void moveTo(Vector3Int moveInt) {
        bool diagonal = Mathf.Sqrt(moveInt.sqrMagnitude) > 1;
        if (moveInt == Vector3Int.zero || (diagonal && remainingMove < 1.5f) || (!diagonal && remainingMove < 1f)) {
            return;
        }

        remainingMove -= diagonal ? 1.5f : 1f;

        if (!map.IsSpaceOccupied(Vector3Int.FloorToInt(transform.position)+moveInt)) {
            transform.position = grid.CellToWorld(grid.WorldToCell(transform.position)+moveInt) + UnitGridOffset;
            UpdateGridPosition();
        }

        if (remainingMove < 1) {
            endMove();
        }
    }

    public void endMove(bool cancelled = false) {
        moveText.gameObject.SetActive(false);
        if (!cancelled || remainingMove < MoveSpeed) {
            remainingActions--;
        }
        remainingMove = 0;
        isCombatMoving = false;
    }

    virtual public void startTargeting(TargetDisplay td) {
        startTargeting(td.type, td.modifiers, td.range, td.radius, td.pathRadius, td.degrees);
    }

    virtual public void startTargeting(targetType type = targetType.None, targetModifiers modifiers = targetModifiers.None, float range = 1f, float radius = 0f, float pathRadius = 0f, int degrees = 1) {
        targetDisplay.type = type;
        targetDisplay.modifiers = modifiers;
        targetDisplay.range = range;
        targetDisplay.radius = radius;
        targetDisplay.pathRadius = pathRadius;
        targetDisplay.degrees = degrees;

        Vector3Int coord = grid.WorldToCell(transform.position);
        targetDisplay.location = new Vector2(coord.x, coord.y);

        if (type == targetType.Melee || type == targetType.Direct || type == targetType.Linear) {
            Vector3Int selectPos = new Vector3Int(coord.x, coord.y, 0);
            
            GameObject go = Instantiate(selectionPrefab, map.transform);
            selection = go.GetComponent<SelectionMarker>();
            selection.Initialize(this, Vector3Int.zero, targetDisplay);
        }

        isTargeting = true;
        if (!isBasicAttacking) isCasting = true;
    }

    virtual public void stopTargeting() {
        selection.DeleteMarkers();
        isTargeting = false;
    }

    public bool CanCast(Skill skill) {
        if (isStunned > 0 || isDead) {
            return false;
        }

        foreach(KeyValuePair<statType, int> cost in skill.costDict) {
            if (statValues[cost.Key]-cost.Value < 0) {
                return false;
            }
        }
        return true;
    }

    public void StartBasicAttack() {
        targetSkill = null;
        isBasicAttacking = true;
        startTargeting(BasicAttackType, targetModifiers.None, BasicAttackRange, BasicAttackRad);
    }

    public void StopBasicAttack() {
        isAttacking = false;
        isBasicAttacking = false;
    }

    public void ExecuteAttack() {
        remainingActions--;

        List<CRUnit> targets = selection.GetTargets();

        if (targetSkill) {
            //if skill has CantTargetCaster remove caster from targets
            if ((targetSkill.modifiers & targetModifiers.CantTargetCaster) > 0) {
                targets.Remove(this);
            }

            targetSkill.cast(targets);
        } else {
            //Perform Basic Attack
            isAttacking = true;

            foreach (CRUnit target in targets) {
                if (target != null) {
                    target.takeDamage(BasicAttackDamage, this, 0);
                }
            }
        }

        stopTargeting();
    }

    public bool IsFriendly(CRUnit unit) {
        CRPlayer player = GetComponent<CRPlayer>();
        CREnemy enemy = GetComponent<CREnemy>();
        CRPlayer unitPlayer = unit.GetComponent<CRPlayer>();
        CREnemy unitEnemy = unit.GetComponent<CREnemy>();
        return ((player && unitPlayer) || (enemy && unitEnemy));
    }

    public StatusEffect ApplyStatusEffect(GameObject effect, int sourceStacks, CRUnit source) {
        if (isDead) return null;
        GameObject newEffectGO = Instantiate(effect, transform.Find("StatusEffects"));
        StatusEffect newStatusEffect = newEffectGO.GetComponent<StatusEffect>();
        newStatusEffect.Initialize();
        if (!newStatusEffect.canStack) {
            newStatusEffect.magnitude *= sourceStacks;
        }
        statusEffects.Add(newStatusEffect);
        newStatusEffect.SetOwner(this);
        newStatusEffect.source = source;
        effectParamaters param = new effectParamaters();
        newStatusEffect.OnEvent(StatusEffectEvent.Start, param);

        //Fire `Player applied status effect to enemy` event
        if (source != this) {
            RelicEventArgs args = new RelicEventArgs();
            args.Initiator = source;
            args.Target = this;
            args.EventType = RelicEventType.PlayerAppliedStatusEffect;
            RelicEvent.Fire(this, args);
        }
        return newStatusEffect;
    }

    public void OnTick(TickType t) {
        for (int i = statusEffects.Count-1; i >= 0; i-- ) {
            StatusEffect se = statusEffects[i];
            se.OnAdvance(t);
            if (se.ticksRemaining <= 0) {
                effectParamaters param = new effectParamaters();
                se.OnEvent(StatusEffectEvent.Expire, param);
                RemoveStatusEffect(se);
            }
        }

        if (t == TickType.StartTurn) {
            shield = 0;
        }
    }
    public void OnStatusEvent (StatusEffectEvent e, effectParamaters param) {
        for (int i = statusEffects.Count-1; i >= 0; i-- ) {
            StatusEffect se = statusEffects[i];
            se.OnEvent(e, param);
        }
    }

    public void RemoveStatusEffect(StatusEffect effect) {
        if (statusEffects.Contains(effect)) {
            effectParamaters param = new effectParamaters();
            effect.OnEvent(StatusEffectEvent.Remove, param);
            statusEffects.Remove(effect);
            Destroy(effect.gameObject);
        }
    }

    public virtual void SetStunned(int newStunnedValue) {
        isStunned = newStunnedValue;
    }

    public void ModifyUnit(modifyParamaters param) {
        switch (param.attribute) {
            case (ModifyAttribute.Armor):
                armor = (int)Util.ApplyOperator(armor, param.intValue, param.operation);
                break;
            case (ModifyAttribute.Strength):
                strength = (int)Util.ApplyOperator(strength, param.intValue, param.operation);
                break;
            case (ModifyAttribute.RemainingActions):
                remainingActions = (int)Util.ApplyOperator(remainingActions, param.intValue, param.operation);
                break;
            case (ModifyAttribute.Stunned):
                SetStunned((int)Util.ApplyOperator(isStunned, param.intValue, param.operation));
                break;
            case (ModifyAttribute.Dead):
                isDead = param.boolValue;
                break;
            case (ModifyAttribute.Shield):
                shield = (int)Util.ApplyOperator(shield, param.intValue, param.operation);
                break;
        }
    }

    //TODO: Do we need this?
    /*public void Cleanse(int amount) {
        for (int i = 0; i < amount; i++) { 
            if (statusEffects[statusEffects.Count-1]) {
                StatusEffect effect = statusEffects[statusEffects.Count-1];
                RemoveStatusEffect(effect);
            }
        }
    }*/
}
