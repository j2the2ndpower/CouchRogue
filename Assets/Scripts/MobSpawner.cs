using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
    [SerializeField] public GameObject[] Monsters;
    [SerializeField] public int[] WeightList;
    [SerializeField] int minSpawnCount = 1;
    [SerializeField] int maxSpawnCount = 1;
    [SerializeField] int spawnAreaRadius = 5;

    private Map map;
    private Game game;

    // Start is called before the first frame update
    void Start() {
        map = FindObjectOfType<Map>();
        game = FindObjectOfType<Game>();
        SpawnMonsters();
    }

    public void SpawnMonsters() {
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount+1);
        for (int i = 0; i < spawnCount; i++) {
            GameObject monster = Instantiate(Monsters[Util.WeightRandom(WeightList)]);
            monster.transform.position = map.GetRandomFreeLocationInRadius(transform.position, spawnAreaRadius);
            game.AI.RegisterEnemy(monster.GetComponent<CREnemy>());
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position + new Vector3(0.5f, 0.5f, 0f), "portal.png", false);
    }
#endif
}
