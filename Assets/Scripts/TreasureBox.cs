using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : Collectible {

    public List<Collectible> TreasurePool;
    public int ItemCount;

    public GameObject treasureOverlay;

    override public void OnInteract(CRPlayer player) {
        List<Collectible> TreasureList = new List<Collectible>();

        for (int i = 0; i < ItemCount; i++) {
            int rnd = Random.Range(0, TreasurePool.Count);
            TreasureList.Add(TreasurePool[rnd]);
        }

        GameObject TO_Object = Instantiate(treasureOverlay, FindObjectOfType<Canvas>().transform);
        TreasureOverlay TO = TO_Object.GetComponent<TreasureOverlay>();

        TO.SetTreasures(TreasureList, player);

        base.OnInteract(player);
    }
}
