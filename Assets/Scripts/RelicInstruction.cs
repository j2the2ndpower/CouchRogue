using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicInstructionType {
    Static = 0,
    Event = 1
}

public enum RelicTarget {
    Owner = 0,
    Initiator = 1,
    Target = 2
}

public enum RelicValueType {
    Static = 0,
    Source = 1
}

public class RelicInstruction : MonoBehaviour {

    [HideInInspector] public Relic relic;
    [HideInInspector] public CRUnit owner;
    public RelicInstructionType type = 0;
    public RelicEventType listenForEvent = 0;

    void Start() {
        if (type == RelicInstructionType.Event) {
            RelicEvent.RelicEventFired += OnEvent;
        }
    }

    virtual public void Equip() {
        if (type == RelicInstructionType.Static) {
            Perform(null);
        }
    }

    virtual public void UnEquip() {
        if (type == RelicInstructionType.Static) {
            UnPerform();
        }
    }

    virtual public void OnEvent(object sender, RelicEventArgs eventArgs) {
        if (eventArgs.EventType == listenForEvent) {
            if (eventArgs.EventType == RelicEventType.PlayerHealedAlly && eventArgs.Target == eventArgs.Initiator) return;
            Perform(eventArgs);
        }
    }

    virtual public void Perform(RelicEventArgs eventArgs) {

    }

    virtual public void UnPerform() {

    }
}
