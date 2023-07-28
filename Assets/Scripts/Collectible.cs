using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : Interactable {
    public string ItemName; //The Displayed Name of the collectible.
    public string ItemDescription; //The Displayed Description of the collectible.
    public TextMeshPro NameText;
    public TextMeshPro DescText;

    override public void Start() {
        base.Start();
        NameText.text = ItemName;
        DescText.text = ItemDescription;
    }

    override public void OnInteract(CRPlayer player) {
        Destroy(gameObject);
    }
}
