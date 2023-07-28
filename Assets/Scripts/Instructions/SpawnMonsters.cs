using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters: Instruction {
    [SerializeField] public GameObject[] Monsters;
    [SerializeField] public int[] WeightList;
    [SerializeField] int minSpawnCount = 1;
    [SerializeField] int maxSpawnCount = 1;
    [SerializeField] int spawnAreaSize = 5;

    private Game game;
    private Room room;

    public override void Perform() {
        game = FindObjectOfType<Game>();
        room = GetComponent<Room>();
        /*Vector2Int spawnLocation = new Vector2Int(Random.Range(room.BottomLeft.x, room.BottomLeft.x + room.RoomSize.x),
                                                  Random.Range(room.BottomLeft.y, room.BottomLeft.y + room.RoomSize.y));*/
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);
        Vector2Int startPos = room.BottomLeft;
        Vector2Int stopPos = startPos+room.RoomSize;
        Vector2Int spawnLocation = map.findRandomEmptySpace(startPos, stopPos, spawnAreaSize, spawnCount);
        

        for (int i = 0; i < spawnCount; i++) {
            GameObject monster = Instantiate(Monsters[Util.WeightRandom(WeightList)]);

            //Look for valid spaces to spawn
            /*Vector3Int[] monsterPos = new Vector3Int[0];
            for (int x = spawnLocation.x-spawnAreaSize; x < spawnLocation.x+spawnAreaSize; x++) {
                for (int y = spawnLocation.y-spawnAreaSize; y < spawnLocation.x+spawnAreaSize; y++) {
                    Vector3Int testPos = new Vector3Int(x, y, 0);
                    if (map.GetTile(map.floorMap, testPos) != null && !map.IsSpaceOccupied(testPos)) {
                        System.Array.Resize(ref monsterPos, monsterPos.Length+1);
                        monsterPos[monsterPos.Length-1] = testPos;
                    }
                }
            }

            if (monsterPos.Length == 0) {
                Debug.LogWarning("Can't find valid space for new monster.");
                continue;
            }*/

            //Choose Random location from valid spaces
            Vector2Int spawnBounds = new Vector2Int(spawnLocation.x+spawnAreaSize, spawnLocation.y+spawnAreaSize);
            Vector2Int finalPos = map.findRandomEmptySpace(spawnLocation, spawnBounds, 1, 1); //monsterPos[Random.Range(0, monsterPos.Length)];
            monster.transform.position = new Vector3(finalPos.x, finalPos.y, 0);
            game.AI.RegisterEnemy(monster.GetComponent<CREnemy>());
        }
    }
}