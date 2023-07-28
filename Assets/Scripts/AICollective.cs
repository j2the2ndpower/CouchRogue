using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollective : MonoBehaviour {
    private List<CREnemy> enemies  = new List<CREnemy>();
    private Game game;
    private Map map;
    public float speed = 1f;
    private MultiplayerZoomCamera cam = null;
    private CREnemy activeEnemy = null;

    private void Start() {
        game = FindObjectOfType<Game>();
        map = FindObjectOfType<Map>();
        cam = FindObjectOfType<MultiplayerZoomCamera>();
    }

    private void Update() {
        bool inCombat = false;
        bool endTurn = true;

        //Update active enemy
        if (activeEnemy != null) {
            if (activeEnemy.remainingActions <= 0 || game.playerTurn || !activeEnemy.inCombat || activeEnemy.isStunned > 0 || activeEnemy.isDead) {
                activeEnemy.turnActive = false;
                activeEnemy.aiState = AIState.idle;
                activeEnemy = null;
            } else {
                chooseAction(activeEnemy);
            }
        }

        //Update each other enemy
        foreach (CREnemy enemy in enemies) {
            if (enemy.inCombat) inCombat = true;

            if (!game.playerTurn && enemy.inCombat && enemy.remainingActions > 0 && enemy.isStunned == 0 && !enemy.isDead) {
                endTurn = false;
                if (activeEnemy == null) {
                    activeEnemy = enemy;
                    cam.ClearTargets();
                    cam.AddTarget(enemy.transform);
                    enemy.turnActive = true;
                }
            }

            enemy.AIAnimator.SetInteger("state", enemy.pendingAIState != AIState.none ? (int)enemy.pendingAIState : (int)enemy.aiState);
            enemy.pendingAIState = AIState.none;
        }

        //End Turn
        if (endTurn && !game.playerTurn) game.EndTurn();

        //End Combat
        if (!inCombat && game.inCombat) game.EndCombat();
    }

    public void RegisterEnemy(CREnemy enemy) {
         enemies.Add(enemy);
    }

    public void RemoveEnemy(CREnemy enemy) {
        enemies.Remove(enemy);
    }

    public void StartTurn() {
        cam.ClearTargets();
        //Enemy might die in this loop so go backwards
        for (int i = enemies.Count-1; i >= 0; i--) {
            CREnemy enemy = enemies[i];
            if (enemy.inCombat) {
                enemy.remainingActions = enemy.BaseActions + enemy.heldActions;
                enemy.heldActions = 0;
                enemy.OnTick(TickType.StartTurn);
            }
        }
    }

    public void EndTurn() {
        foreach (CREnemy enemy in enemies) {
            enemy.turnActive = false;
            if (enemy.inCombat) enemy.OnTick(TickType.EndTurn);
        }
    }

    public void EndCombat() {
        foreach (CREnemy enemy in enemies) {
            enemy.inCombat = false;
            if (enemy.isCombatMoving) {
                enemy.endMove();
            }
            enemy.turnActive = false;
        }
    }

    public bool CanEscapeCombat(CRPlayer player) {
        bool canEscape = true;
        foreach (CREnemy enemy in enemies) {
            if (enemy.inCombat && !enemy.PlayerCanEscape(player)) {
                canEscape = false;
            }
        }

        return canEscape;
    }

    public Vector3Int getMoveTarget(CRUnit unit) {
        Vector3Int targetPosition = Vector3Int.zero;
        List<Vector3Int> openPositions = new List<Vector3Int>();
        foreach (CRPlayer player in game.players) {
            if (player.isDead) continue;
            List<Vector3Int> adjacentSquares = new List<Vector3Int>() {
                new Vector3Int(0, -1, 0),
                new Vector3Int(0, 1, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, -1, 0),
                new Vector3Int(-1, 1, 0),
                new Vector3Int(1, -1, 0),
                new Vector3Int(1, 1, 0)
            };
            foreach (Vector3Int adjacentSquare in adjacentSquares) {
                Vector3Int position = adjacentSquare + player.gridPosition;
                if (!map.IsSpaceOccupied(position)) {
                    openPositions.Add(position);
                }
            }
        }

        foreach (Vector3Int openPosition in openPositions) {
            if (targetPosition == Vector3Int.zero || (unit.gridPosition - openPosition).magnitude < (unit.gridPosition - targetPosition).magnitude) {
                targetPosition = openPosition;
            }
        }

        if (targetPosition == Vector3Int.zero) {
            targetPosition = unit.gridPosition;
        }

        return targetPosition;
    }

    public void chooseAction(CREnemy enemy) {
        if (enemy.goalState != AIState.idle) return;

        //Determine desired state
        /*if ((HP < 30%  && enemy.canFlee && !enemy.canHeal) || enemy.isCoward) flee
        if (HP < 30% && enemy.canHeal && !enemy.isMartyr)  healSelf
        if (OtherEnemyHasLowHP && enemy.canHeal) healOtherEnemy
        if (PlayerisLowHP && enemy.bloodThirst) attackLowHPPlayer*/

        if (!enemy.isCombatMoving && enemy.remainingActions > 0) {
            enemy.aiState = AIState.attack;
        } else if (enemy.remainingActions > 0) {
            enemy.aiState = AIState.move;
        }
    }
}
