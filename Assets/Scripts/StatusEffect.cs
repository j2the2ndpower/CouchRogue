using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct effectParamaters {
    public float Damage;
    public float buffedDamage;
    public float finalDamage;
    public CRUnit attacker;
}

public enum TickType {
    StartTurn,
    EndTurn,
}

public class StatusEffect : MonoBehaviour {
    public string Name;
    public Sprite icon;
    public int frequency = 1;
    public int duration;
    public bool permanent;
    public bool canStack = false;
    public int magnitude = 1;
    public bool negative = false;
    public bool diminishing = false;
    public TickType tickType;
    public bool hidden = false;

    [HideInInspector] public int ticksRemaining;
    [HideInInspector] int advanceCount = 0;

    private CRUnit owner;
    [HideInInspector] public CRUnit source;
    [HideInInspector] public StatusEffectInstruction[] instructions;

    [HideInInspector] public StatusEffectStack stack;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        instructions = GetComponents<StatusEffectInstruction>();
        foreach(StatusEffectInstruction instruction in instructions) {
            instruction.owner = owner;
            instruction.statusEffect = this;
        }        
    }

    public void SetOwner(CRUnit owner) {
        this.owner = owner;
        foreach(StatusEffectInstruction instruction in instructions) {
            instruction.owner = owner;
        }
    }

    public void OnAdvance(TickType t) {
        if (t != tickType) return;
        if (++advanceCount >= frequency) {
            effectParamaters param = new effectParamaters();
            OnEvent(StatusEffectEvent.Tick, param);
        }
    }

    public void OnEvent(StatusEffectEvent eventType, effectParamaters param) {
        if (eventType == StatusEffectEvent.Start) {
            bool createEffect = true;
            StatusEffect previousStatusEffect = null;
            foreach (StatusEffect status in owner.statusEffects) {
                if (status.Name == Name && status != this) {
                    previousStatusEffect = status;
                }
            }

            if (previousStatusEffect) { 
                if (!canStack) {
                    previousStatusEffect.ticksRemaining = duration;
                    owner.RemoveStatusEffect(this);
                    createEffect = false;
                } else {
                    if (diminishing || previousStatusEffect.ticksRemaining == duration) {
                        previousStatusEffect.SetMagnitude(previousStatusEffect.magnitude + magnitude);
                        owner.RemoveStatusEffect(this);
                        createEffect = false;
                    }
                }
            }
                
            if (createEffect) {
                ticksRemaining = duration;
                advanceCount = 0;
                if (!hidden) {
                    GameObject stackGO = Instantiate(owner.statusStackPrefab, owner.statusStackParent);
                    stack = stackGO.GetComponent<StatusEffectStack>();
                    stack.SetSprite(icon);
                    stack.SetCount(duration);
                    if (magnitude > 1) { 
                        stack.ShowStacks(true);
                        stack.SetStacks(magnitude);
                    }
                }
            }
        }

        InstructionEvent(eventType, param);

        if (eventType == StatusEffectEvent.Tick) {
            ticksRemaining--;
            if (diminishing){
              SetMagnitude(ticksRemaining);
            }
            if (stack) stack.SetCount(ticksRemaining);
            advanceCount = 0;
        }

        if (eventType == StatusEffectEvent.Remove) {
            if (stack) Destroy(stack.gameObject);
        }
    }
    public void SetMagnitude(int value){
        InstructionEvent(StatusEffectEvent.Remove, new effectParamaters());
        magnitude = value;
        if(diminishing){
            ticksRemaining = magnitude;
        }
        InstructionEvent(StatusEffectEvent.Start, new effectParamaters());
        UpdateStackUI();
    }
    public void UpdateStackUI(){
        if (stack) {
            stack.SetStacks(magnitude);
            stack.SetCount(ticksRemaining);
            stack.ShowStacks(!diminishing);
        }
    }
    public void InstructionEvent(StatusEffectEvent e, effectParamaters paramaters) {
        foreach(StatusEffectInstruction instruction in instructions) {
            if(instruction.statusEvent == e) {
                instruction.OnEvent(paramaters);
            }
        }
    }
}
