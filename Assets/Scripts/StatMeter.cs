using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatMeter : MonoBehaviour {
    [SerializeField] public Image empty;
    [SerializeField] public Image full;
    [SerializeField] public Image mask;
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public Image ShieldEdgeMask;
    [SerializeField] public Image ShieldIcon;
    [SerializeField] public Image ShieldMeter;
    [SerializeField] public TextMeshProUGUI ShieldText;


    private float baseWidth = 42;

    public void UpdateValue(float value, float max, float shield = 0f) {
        mask.transform.localPosition = new Vector3((max-value)/max*baseWidth*-1, mask.transform.localPosition.y, mask.transform.localPosition.z);
        full.transform.localPosition = new Vector3((max-value)/max*baseWidth, full.transform.localPosition.y, full.transform.localPosition.z);
        text.SetText(value.ToString() + " / " + max.ToString());

        ShieldText.SetText(shield.ToString());
        shield = Mathf.Clamp(shield, 0, max);
        ShieldEdgeMask.rectTransform.sizeDelta = new Vector2((shield/max)*baseWidth , 6.2f);
        ShieldEdgeMask.gameObject.SetActive(shield > 0);
        ShieldText.gameObject.SetActive(shield > 0);
        ShieldIcon.gameObject.SetActive(shield > 0);
    }

    public void SetColor(Color color, Color shieldColor) {
        //empty.color = new Color(color.r, color.g, color.b, 0x99);
        full.color = color;
        // Wanna change the shield color?
        // ShieldIcon.color = playerColor;
        // ShieldMeter.color = playerColor;
    }
}
