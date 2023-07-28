using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class RadialMenuItem : MonoBehaviour {
    [SerializeField] public bool isEnabled = true;
    [SerializeField] public string Text = "";
    [SerializeField] public string SubText = "";
    
    [HideInInspector] public CRPlayer player;
    [HideInInspector] public RadialMenu menu;
    [HideInInspector] public RadialMenuScreen subMenu;
    [HideInInspector] public int slot;
    public GameObject disableObj;
    protected Game game;
    
    abstract public void DoAction();

    virtual public void Start() {
        game = FindObjectOfType<Game>();
        SetEnabled(isEnabled);
    }

    public void SetEnabled(bool enabled) {
        if (!disableObj) return;
        
        isEnabled = enabled;
        disableObj.SetActive(!enabled);
    }

    public void SetPlayer(CRPlayer player) {
        this.player = player;
    }

    public void SetDisableObj(GameObject disobj) {
        this.disableObj = disobj;
    }

    public void SetMenu(RadialMenu menu) {
        this.menu = menu;
    }

    public void SetSubMenu(RadialMenuScreen subMenu) {
        this.subMenu = subMenu;
    }

    public void SetSlot(int slot) {
        this.slot = slot;
    }

    virtual public void Initialize() {

    }
}
