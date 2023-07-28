using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RuneType {
    None = 0,
    Ages = 1,
    Brutality = 2,
    Cosmos = 3,
    Decay = 4,
    Elements = 5,
    Gluttony = 6,
    Growth = 7,
    Instability = 8,
    Nomicon = 9
}

public enum SkillKeyword {
    None = 0,
    Knockback = 1,
    Rectify = 2,
    Echo = 3,
    Repeat = 4,
}

public class Skill : MonoBehaviour {
    public string Name;
    public string description;
    public Sprite icon;
    public List<int> costs;
    public List<statType> costTypes;
    public Dictionary<statType, int> costDict = new Dictionary<statType, int>();
    public targetType type;
    public int degrees;
    public float radius;
    public float pathRadius;
    public float range;
    public targetModifiers modifiers;
    public int skillEffectiveness;
    [SerializeField] public float increasedCritChance = 0;

    public int runeCapacity = 3;

    public List<RuneType> runes;

    public List<RuneEffect> runeEffects;

    public TargetDisplay targetDisplay;
    [HideInInspector] public CRUnit caster;

    [HideInInspector] public RadialMenuItem menuItem;

    public bool displayOnly = false;

    public GameObject effect;
    [HideInInspector] public bool hideEffect = false;

    public List<AIState> AIUseInStates;
    public List<SkillKeyword> keywords;
    [HideInInspector] public SkillInstruction[] instructions;
    [SerializeField] private float duration = 0;
    private float timeSinceCast;
    private bool isBeingCast;
    private int skillRepeats = 0;
    private List<CRUnit> previousTargets = new List<CRUnit>();

    public int slot = 0;

    public void Start() {
        
        instructions = GetComponents<SkillInstruction>();
        foreach(SkillInstruction instruction in instructions) {
            instruction.skill = this;
            instruction.caster = caster;
        }
        for(int i = 0; i < costs.Count; i++) {
            costDict.Add(costTypes[i], costs[i]);
        }

        // Add rune effects if they exist
        for (int i = 0; i < runes.Count; i++) {
            if (runes[i] != RuneType.None) {
                runeEffects[(int)runes[i]].Attach();
            }
        }
        UpdateTargetDisplay();
    }

    virtual public void cast(List<CRUnit> targets = null) {
        //Spawn Visual Effects
        if (effect && !hideEffect) {
            GameObject ef;
            if (targetDisplay.type == targetType.Self) {
                ef = GameObject.Instantiate(effect, caster.transform.position, Quaternion.identity);
            } else if (!caster.inCombat) {
                ef = GameObject.Instantiate(effect, ((CRPlayer)caster).oocTarget.position, Quaternion.identity);
            } else {
                Vector2 effectLocation = caster.targetDisplay.location + new Vector2(caster.UnitGridOffset.x, caster.UnitGridOffset.y);
                ef = GameObject.Instantiate(effect, effectLocation, Quaternion.identity);
            }
            ef.GetComponent<AfterEffect>().owner = caster;
        }

        //Spend Resoures
        if (!isBeingCast) {
            foreach(KeyValuePair<statType, int> cost in costDict) {
                caster.spendResource(cost.Key, cost.Value);
            }
        }

        //Perform Instructions
        foreach(SkillInstruction instruction in instructions) {
            instruction.Cast(targets);
        }

        //Apply Status Effects
        foreach(RuneType rt in runes) {
            if (rt != RuneType.None) {
                runeEffects[(int)rt].OnCast(targets);
            }
        }

        isBeingCast = true;
        timeSinceCast = 0;
        previousTargets = targets;
    }
    
    virtual public void cancel() {}

    public void SetCaster(CRUnit unit) {
        this.caster = unit;

        foreach(SkillInstruction instruction in instructions) {
            instruction.caster = caster;
        }
    }

    virtual public void Consume() {
        menuItem.subMenu.ClearSelectionAtSlot(menuItem.slot);
    }

    public bool AddRune(RuneType type) {
        RuneSlot[] runeSlots = ((CRPlayer)caster).skillMenu.menuItems[slot].transform.GetComponentsInChildren<RuneSlot>();
        for (int i = 0; i < runes.Count; i++) {
            if (runes[i] == RuneType.None) {
                runes[i] = type;
                runeSlots[i].SetRune(type);
                runeEffects[(int)type].Attach();
                return true;
            }
        }
        return false;
    }

    public void UpdateTargetDisplay() {
        targetDisplay.range = range;
        targetDisplay.radius = radius;
        targetDisplay.pathRadius = pathRadius;
        targetDisplay.type = type;
        targetDisplay.degrees = degrees;
        targetDisplay.modifiers = modifiers;
    }

    private void Update() {
        if (isBeingCast) {
            timeSinceCast += Time.deltaTime;
            if (timeSinceCast >= duration) {
                EndCast();
            }
        }
    }

    public void EndCast() {
        if (skillRepeats < GetKeywordCount(SkillKeyword.Repeat) && ((caster.inCombat && caster.game.playerTurn) || !caster.inCombat)) {
            skillRepeats++;
            cast(previousTargets);
        } else {
            caster.isCasting = false;
            timeSinceCast = 0;
            isBeingCast = false;
            skillRepeats = 0;
        }
    }

    public int GetKeywordCount(SkillKeyword keyword) {
        int returnVal = 0;
        foreach (SkillKeyword k in keywords) {
            if (k == keyword) {
                returnVal++;
            }
        }
        return returnVal;
    }

    public int GetRuneCount (RuneType type) {
        int returnVal = 0;
        for (int i = 0; i < runes.Count; i++) {
            if (runes[i] == type) {
                returnVal++;
            }
        }
        return returnVal;
    }
}
