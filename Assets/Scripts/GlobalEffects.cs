using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffects : MonoBehaviour {
    public Dictionary<string, GameObject> effect;
    [SerializeField] public List<string> effectNames;
    [SerializeField] public List<GameObject> effectPrefabs;

    private void Start() {
        effect = new Dictionary<string, GameObject>();
        for (int i = 0; i < effectNames.Count; i++) {
            effect.Add(effectNames[i],effectPrefabs[i]);
        }
    }
}
