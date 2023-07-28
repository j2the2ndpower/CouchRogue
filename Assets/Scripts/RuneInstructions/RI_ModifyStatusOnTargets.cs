using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags] public enum TargetAlignment {
    // Inside Target Array
    Affected = 64,
    Friendly = 1,
    Enemy = 2,
    NotSelf = 4,
    Dead = 8,

    // Outside of Target Array
    NotAffected = 128,
    Self = 16,
    InRadius = 32,
 }

public enum StatusAction {
    Apply = 0,
    Remove = 1
}

public enum StatusEffectType {
    Positive = 0,
    Negative = 1,
    All = 2
}

public class RI_ModifyStatusOnTargets : RuneInstruction {
    public TargetAlignment applyToAlignment;
    public StatusAction action;
    public StatusEffectType effectType;
    public GameObject statusPrefab;
    public bool overrideMagnitude;
    public int magnitudeOverride;
    public bool overrideDuration;
    public int durationOverride;
    public float radius;
    //OR
    public bool RandomStatusEffect = false;
    
    override public void Attach() {}

    override public void OnCast(List<CRUnit> targets) {
        foreach(CRUnit target in Util.GetTargetsOfType(applyToAlignment, targets, caster, radius)) {
            if (action == StatusAction.Apply) {
                StatusEffect s = target.ApplyStatusEffect(GetApplyEffect(target), skill.GetRuneCount(runeEffect.type), caster);
                UpdateStatusEffect(s);
            } else {
                StatusEffect removeEffect = GetRemoveEffect(target);
                if (removeEffect != null) {
                    target.RemoveStatusEffect(removeEffect);
                }
            }
        }
    }

    private GameObject GetApplyEffect(CRUnit target) {
        GameObject applyEffect = statusPrefab;
        if (RandomStatusEffect) {
            Game game = FindObjectOfType<Game>();
            if (effectType == StatusEffectType.All) {
                applyEffect = game.allStatusEffects[UnityEngine.Random.Range(0, game.allStatusEffects.Count)];
            } else if (effectType == StatusEffectType.Negative) {
                applyEffect = game.negativeStatusEffects[UnityEngine.Random.Range(0, game.negativeStatusEffects.Count)];
            } else if (effectType == StatusEffectType.Positive) {
                applyEffect = game.positiveStatusEffects[UnityEngine.Random.Range(0, game.positiveStatusEffects.Count)];
            }
        }

        return applyEffect;
    }

    private StatusEffect GetRemoveEffect(CRUnit target) {
        StatusEffect removeEffect = null;
        // Find existing effect on target
        if (RandomStatusEffect) {
            List<StatusEffect> validStatusEffects = new List<StatusEffect>();
            foreach (StatusEffect se in target.statusEffects) {
                if (effectType == StatusEffectType.All || Convert.ToInt32(se.negative) == (int)effectType) {
                    validStatusEffects.Add(se);
                }
            }

            if (validStatusEffects.Count > 0) {
                removeEffect = validStatusEffects[UnityEngine.Random.Range(0, validStatusEffects.Count)];
            }
        } else {
            if (statusPrefab != null) {
                foreach (StatusEffect se in target.statusEffects) {
                    if (se.name == statusPrefab.name) {
                        removeEffect = se;
                    }
                }
            }
        }

        return removeEffect;
    }

    public void UpdateStatusEffect(StatusEffect effect) {
        if (overrideMagnitude) effect.SetMagnitude(magnitudeOverride);
        if (overrideDuration) effect.ticksRemaining = durationOverride;
    }

}
