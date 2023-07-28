using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicEventType {
    None = 0,
    PlayerHealedAlly = 1,
    PlayerAppliedStatusEffect = 2
}

public class RelicEventArgs : EventArgs {
    public RelicEventType EventType { get; set; }
    public CRUnit Initiator { get; set; }
    public CRUnit Target { get; set; }
    public float FloatValue { get; set; }
}

public static class RelicEvent {
    public static event EventHandler<RelicEventArgs> RelicEventFired;

    public static void Fire(object sender, RelicEventArgs e) {
        EventHandler<RelicEventArgs> handler = RelicEventFired;
        if (handler != null) {
            handler(sender, e);
        }
    }
}
