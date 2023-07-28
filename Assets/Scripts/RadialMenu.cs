using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RadialMenu: MonoBehaviour {
    [SerializeField] public CRPlayer player;
    [SerializeField] public TextMeshPro tooltip;
    [SerializeField] public TextMeshPro subText;
    [SerializeField] public string DefaultScreen;
    [SerializeField] public RadialMenuScreen[] AvailableScreens;
    [SerializeField] public RuneSlot runePreview;
    private Dictionary<string, RadialMenuScreen> screens = new Dictionary<string, RadialMenuScreen>();
    private string ActiveScreen = "";
    [HideInInspector] public float lastAngle = 0f;
    [HideInInspector] public bool moveIsHeld = false;
    [HideInInspector] public Vector2 lastMenuDir;
    [HideInInspector] public Collectible interactingRune;

    public void Start() {
        foreach (RadialMenuScreen screen in AvailableScreens) {
            screens.Add(screen.gameObject.name, screen);

            screen.SetPlayer(player);
            screen.SetMenu(this);
            screen.Initialize();
        }
    }

    public void OnRadialMove(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            lastAngle = Util.Angle(ctx.ReadValue<Vector2>());
            lastMenuDir = ctx.ReadValue<Vector2>();
            moveIsHeld = true;
        } else if (ctx.canceled) {
            moveIsHeld = false;
        }

        if (screens.ContainsKey(ActiveScreen) && screens[ActiveScreen].isActiveAndEnabled) {
            screens[ActiveScreen].OnRadialMove(ctx);
        }
    }

    public void OnRadialSelect(InputAction.CallbackContext ctx) {
        if (screens.ContainsKey(ActiveScreen) && screens[ActiveScreen].isActiveAndEnabled) {
            screens[ActiveScreen].OnRadialSelect(ctx);
        }
    }

    public void OnRadialCancel(InputAction.CallbackContext ctx) {
        if (ctx.started && screens.ContainsKey(ActiveScreen) && player.inMenu) {
            if (ActiveScreen == DefaultScreen) {
                player.CloseRadialMenu();
            } else if (player.assigningRune) {
                HideRunePreview(false);
            } else if (screens.ContainsKey(screens[ActiveScreen].ParentScreen)) {
                SetActiveScreen(screens[ActiveScreen].ParentScreen);
            }
        }
    }

    //This really only happens when player opens menu the first time
    public void ShowScreen() {
        ActiveScreen = DefaultScreen;
        screens[ActiveScreen].Activate(false, 0f);
        SetTooltip("", "");
        SetTooltipActive(true);
        
    }

    //This only happens when player lets go of menu button
    public bool HideScreen() {
        bool closedMainScreen = (ActiveScreen == DefaultScreen);
        screens[ActiveScreen].Deactivate();
        SetTooltipActive(false);
        return closedMainScreen;
    }

    //This happens when choosing subMenu
    public void SetActiveScreen(string ScreenName) {
        screens[ActiveScreen].Deactivate();
        ActiveScreen = ScreenName;
        screens[ActiveScreen].Activate(true, lastAngle);
    }

    public void SetTooltip(string text, string sub) {
        tooltip.gameObject.SetActive(true);
        subText.gameObject.SetActive(true);
        tooltip.SetText(text);
        subText.SetText(sub);
    }

    public void SetTooltipActive(bool active) {
        tooltip.gameObject.SetActive(active);
        subText.gameObject.SetActive(active);
    }

    public void ShowRunePreview(RuneType type, Collectible rune) {
        interactingRune = rune;
        interactingRune.HidePrompt();
        runePreview.gameObject.SetActive(true);
        player.skillMenu.gameObject.SetActive(true);
        player.ShowRadialMenu();
        SetActiveScreen("SkillMenu");
        screens[ActiveScreen].ClearSelection();
        player.assigningRune = true;
        runePreview.SetRune(type);
        player.EnableRadialMenuActionMap(true);
    }

    public void HideRunePreview(bool destroy) {
        if (interactingRune) {
            interactingRune.ShowPrompt();
            if (destroy) Destroy(interactingRune.gameObject);
            interactingRune = null;
        }
        runePreview.gameObject.SetActive(false);
        player.skillMenu.gameObject.SetActive(false);
        player.CloseRadialMenu();
        player.assigningRune = false;
        player.EnableRadialMenuActionMap(false);
    }
}
