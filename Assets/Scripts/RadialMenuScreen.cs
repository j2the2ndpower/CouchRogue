using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenuScreen : MonoBehaviour {
    [SerializeField] private int menuSize = 4;
    [SerializeField] public SpriteRenderer selector;
    [SerializeField] public SpriteRenderer pointer;
    [SerializeField] public GameObject disabledPrefab;
    [SerializeField] public string ParentScreen;
    [SerializeField] public GameObject RuneSlotPrefab;
    [SerializeField] public List<RadialMenuItem> menuItems;

    private SpriteRenderer spriteRenderer;
    private CRPlayer player;
    private RadialMenu menu;
    private int SelectedItem;

    public void Initialize() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null) {
            spriteRenderer.color = player.unitColor;
            selector.color = player.unitColor;
            pointer.color = player.unitColor;

            int itemCount = 0;
            for (int i = 0; i < menuItems.Count; i++) {
                SetupNewMenuItem(menuItems[i], i);
                itemCount++;
            }

            if (name == "SkillMenu") {
                //Add skills to list
                foreach(GameObject skill in player.startingSkills) {
                    SetSkillAtFirstOpenSelection(skill);
                }
            }
        }
    }

    public void SetupNewMenuItem(RadialMenuItem item, int slot) {
        GameObject newDisableObj = Instantiate(disabledPrefab, menu.transform);
        newDisableObj.transform.rotation = Quaternion.AngleAxis(-slot * (360f / menuSize), Vector3.forward);
        newDisableObj.SetActive(false);
        newDisableObj.transform.parent = item.transform;

        item.Initialize();
        item.SetDisableObj(newDisableObj);
        item.SetPlayer(player);
        item.SetMenu(menu);
        item.SetSubMenu(this);
        item.SetSlot(slot);
        
    }

    public void SetupNewRuneSlots(SkillMenuItem item) {
        int slot = item.slot;
        Skill skill = item.skill;
        if (RuneSlotPrefab && skill != null) {
            int runeCount = skill.runeCapacity;
            for (int i = 0; i < runeCount; i++) {
                GameObject newRuneSlot = Instantiate(RuneSlotPrefab, menu.transform);
                float offset = (360f / menuSize / (runeCount + 1)) * (i+1);
                float startAngle = 22.5f;
                newRuneSlot.transform.rotation = Quaternion.AngleAxis(-slot * (360f / menuSize) - offset + startAngle, Vector3.forward);
                newRuneSlot.transform.parent = menuItems[slot].transform;
                newRuneSlot.GetComponent<RuneSlot>().player = player;
                if (skill.runes.Count < i || skill.runes[i] == RuneType.None) {
                    newRuneSlot.GetComponent<RuneSlot>().SetRune(RuneType.None);
                } else {
                    newRuneSlot.GetComponent<RuneSlot>().SetRune(skill.runes[i]);
                }
            }
        }
    }

    public void RemoveRuneSlots(SkillMenuItem item) {
        foreach(RuneSlot runeSlot in GetComponentsInChildren<RuneSlot>()) {
            Destroy(runeSlot.gameObject);
        }
    }

    public void OnRadialMove(InputAction.CallbackContext ctx) {
        Vector2 moveVector = ctx.ReadValue<Vector2>();
        float moveAngle = Util.Angle(moveVector);
        if (ctx.started) {
            selector.gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
            menu.SetTooltipActive(true);
        } else if (ctx.canceled) {
            ClearSelection();
            menu.SetTooltipActive(false);
        } else if (ctx.performed) {
            UpdateSelection(moveAngle);
        }
        for (int i = 0; i < menuItems.Count; i++) {
            if ((i != SelectedItem) || ctx.canceled) {
                menuItems[i].transform.localScale = new Vector3(1.0f, 1.0f, 1);
            } else {
                menuItems[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }
        }
    }

    public void ClearSelection() {
        selector.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        if (SelectedItem >= 0) {
            SkillMenuItem smi = menuItems[SelectedItem].GetComponent<SkillMenuItem>();
            if (smi != null)  smi.hideRuneSlots();
            SelectedItem = -1;
        }
    }

    public void UpdateSelection(float angle) {
        SkillMenuItem smi = null; 
        if (SelectedItem >= 0 && SelectedItem < menuItems.Count) {
            smi = menuItems[SelectedItem].GetComponent<SkillMenuItem>();
            if (smi != null)  smi.hideRuneSlots();
        }

        selector.transform.rotation = Quaternion.AngleAxis(-Util.SnapTo(angle, 360f/menuSize), Vector3.forward);
        pointer.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        SelectedItem = (int)Util.SnapTo(angle, 360f/menuSize)/(360/menuSize)%menuSize;
        RadialMenuItem selectedMenuItem = menuItems[SelectedItem];
        if (!selectedMenuItem.isEnabled) {
            ClearSelection();
        } else {
            selector.gameObject.SetActive(true);
        }

        Skill skill = null;
        if (smi) { skill = smi.skill; }
        if (!player.assigningRune) {
            menu.SetTooltip(selectedMenuItem.Text, selectedMenuItem.SubText);
        } else if (skill && skill.runeEffects.Count >= (int)menu.runePreview.runeType) {
            menu.SetTooltip(skill.Name, skill.runeEffects[(int)menu.runePreview.runeType].description);
        } else {
            menu.SetTooltip("", "");
        }
        if (!pointer.gameObject.activeSelf) {
            pointer.gameObject.SetActive(true);
        }

        if (selectedMenuItem && SelectedItem < menuItems.Count && SelectedItem >= 0) {
            smi = menuItems[SelectedItem].GetComponent<SkillMenuItem>();
            if (smi != null)  smi.showRuneSlots();
        }
    }

    public void OnRadialSelect(InputAction.CallbackContext ctx) {
        if (ctx.started && player != null && player.inMenu && SelectedItem >= 0) {
            if (player.assigningRune) {
                SkillMenuItem smi = menuItems[SelectedItem].GetComponent<SkillMenuItem>();
                Skill s = smi.skill;
                if (s.AddRune(menu.runePreview.runeType)) {
                    menu.HideRunePreview(true);
                } else {
                    //TODO: Add error noise
                }
            } else {
                menuItems[SelectedItem].DoAction();
            }
        }
    }

    public void SetPlayer(CRPlayer player) {
        this.player = player;
    }

    public void SetMenu(RadialMenu menu) {
        this.menu = menu;
    }

    public void Activate(bool subMenu, float currentAngle) {
        this.gameObject.SetActive(true);
        if (subMenu) {
            this.pointer.gameObject.SetActive(true);
            this.selector.gameObject.SetActive(true);
            UpdateSelection(currentAngle);
        }
    }

    public void Deactivate() {
        this.selector.gameObject.SetActive(false);
        this.pointer.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        for (int i = 0; i < menuItems.Count; i++) {
            menuItems[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public GameObject SetSlotToSkill(GameObject newSkill, int slot) {
        SkillMenuItem smi = menuItems[slot].GetComponent<SkillMenuItem>();
        if (smi == null) return null;

        GameObject newSkillObj = Instantiate(newSkill, player.transform.Find("Skills"));
        Skill s = newSkillObj.GetComponent<Skill>();
        smi.SetSkill(s);
        s.slot = slot;
        
        /*RadialMenuItem newSkillItem = newSkillObj.GetComponent<RadialMenuItem>();
        menuItems[slot] = newSkillItem;
        SetupNewMenuItem(newSkillItem, slot);*/
        return newSkillObj;
    }

    public void ClearSelectionAtSlot(int slot) {
        SkillMenuItem smi = menuItems[slot].GetComponent<SkillMenuItem>();
        if (smi == null) return;

        smi.ClearSkill();
    }

    public bool SetSkillAtFirstOpenSelection(GameObject newSkill) {
        for(int i = 0; i < menuItems.Count; i++) {
            SkillMenuItem smi = menuItems[i].GetComponent<SkillMenuItem>();

            if (smi && !smi.skill) { //If true, we've found our slot
                SetSlotToSkill(newSkill, i);
                return true;
            }
        }
        //We've failed to find an open slot.
        return false;
    }
}
