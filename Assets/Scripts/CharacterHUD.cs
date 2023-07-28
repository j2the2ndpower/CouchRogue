using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour {
    [SerializeField] public VerticalLayoutGroup layout;
    [SerializeField] public GridLayoutGroup relicLayout;
    [SerializeField] public GameObject meterPrefab;
    [SerializeField] public GameObject relicHUDPrefab;
    public GameObject DescPanel;
    public Text NameText;
    public Text DescText;
    public GameObject PromptText;
    public CRPlayer player;
    private int PlayerIndex;
    private Dictionary<statType, StatMeter> meters = new Dictionary<statType, StatMeter>();

    public void SetPlayer(CRPlayer p, int index) {
        player = p;
        PlayerIndex = index;
        GetComponent<Image>().color = player.unitColor;

        foreach (KeyValuePair<statType, StatMeter> entry in meters) {
            Destroy(entry.Value.gameObject);
        }
        meters.Clear();

        foreach (statType type in player.stats) {
            GameObject newMeterGO = Instantiate(meterPrefab, layout.transform);
            StatMeter newMeter = newMeterGO.GetComponent<StatMeter>();
            meters.Add(type, newMeter);
            newMeter.SetColor(player.game.getStatColor(type), player.unitColor);
        }
    }

    public void UpdateRelics() {
        foreach(RelicHUD oldRelicHUDGO in relicLayout.GetComponentsInChildren<RelicHUD>()) {
            Destroy(oldRelicHUDGO.gameObject);
        }
        
        foreach (GameObject relicGO in player.OwnedRelics) {
            GameObject relicHUDGo = Instantiate(relicHUDPrefab, Vector3.zero, Quaternion.identity, relicLayout.transform);
            relicHUDGo.GetComponent<RelicHUD>().SetSprite(relicGO.GetComponent<Relic>().icon);
        }
    }

    public void Update() {
        foreach (KeyValuePair<statType, StatMeter> entry in meters) {
            float shieldValue = 0f;
            if (entry.Key == statType.Health) { shieldValue = player.shield; }
            entry.Value.UpdateValue(player.getStat(entry.Key), player.getMaxStat(entry.Key), shieldValue);
        }
    }

    public void ShowDescPanel(bool b, string Name, string Desc, bool ShowPrompt) {
        DescPanel.SetActive(b);
        NameText.text = Name;
        DescText.text = Desc;
        PromptText.SetActive(ShowPrompt);
    }
}
