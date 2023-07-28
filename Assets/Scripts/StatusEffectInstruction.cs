using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectEvent {
    Start = 1,
    Expire = 2,
    Remove = 3,
    Tick = 4,
    TakeDamage = 5,
    ChangeAttribute = 6,
}

abstract public class StatusEffectInstruction : MonoBehaviour {
    [HideInInspector] public CRUnit owner;
    [HideInInspector] public StatusEffect statusEffect;
    public StatusEffectEvent statusEvent;

    abstract public void OnEvent(effectParamaters param);
}
