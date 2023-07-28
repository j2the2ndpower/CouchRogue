using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuItem : RadialMenuItem {
    public Skill skill;
    private SpriteRenderer spriteRenderer;

    override public void Initialize() {
        base.Initialize();
        skill = this.GetComponent<Skill>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player != null && skill != null) {
            SetSkill(skill);
        }
    }

    private void Update() {
        if (!player || !skill) {
            SetEnabled(false);
        } else {
            SetEnabled(!skill.displayOnly && (player.CanCast(skill) || player.assigningRune) && ((game.playerTurn && player.remainingActions > 0) || !player.inCombat));
        }
    }

    private void OnEnable() {
        
    }

    public override void DoAction() {
        if (!isEnabled) {
            //Make Error Noise or something
            return;
        }

        if (skill.targetDisplay.type == targetType.None ) {
            // cast
            skill.cast();
        } else if (skill.targetDisplay.type == targetType.Self) {
            skill.cast(new List<CRUnit>() { player });
        } else {
            player.targetSkill = skill;
            player.startTargeting(skill.targetDisplay);
        }
    }

    public void hideRuneSlots() {
        foreach (RuneSlot s in GetComponentsInChildren<RuneSlot>()) {
            s.hide();
        }
    }

    public void showRuneSlots() {
        foreach (RuneSlot s in GetComponentsInChildren<RuneSlot>()) {
            s.show();
        }
    }

    public void SetSkill(Skill newSkill) {
        skill = newSkill;
        Text = skill.Name;
        SubText = skill.description;
        skill.SetCaster(player); 
        if (spriteRenderer) {
            spriteRenderer.sprite = newSkill.icon;
        }
        skill.menuItem = this;
        subMenu.SetupNewRuneSlots(this);
    }

    public void ClearSkill() {
        subMenu.RemoveRuneSlots(this);
        Destroy(skill.gameObject);
        skill = null;
        Text = "";
        SubText = "";
        if (spriteRenderer)
            spriteRenderer.sprite = null;
        
    }
}
