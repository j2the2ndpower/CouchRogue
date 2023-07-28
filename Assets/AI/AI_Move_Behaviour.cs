using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Move_Behaviour : StateMachineBehaviour {
    CREnemy unit;
    AICollective aiCollective;
    Vector3Int moveTarget;
    float timeSinceLastMove = 0f;
    const float moveFrequency = 1.5f;
    List<Vector2Int> path = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Game game = FindObjectOfType<Game>();
        Map map = FindObjectOfType<Map>();
        unit = animator.transform.parent.gameObject.GetComponent<CREnemy>();
        aiCollective = FindObjectOfType<AICollective>();

        //Ask the AI Collective to find a place to move to
        Vector3Int target = aiCollective.getMoveTarget(unit);

        AStar pf = new AStar(map, new Vector2Int(unit.gridPosition.x, unit.gridPosition.y), new Vector2Int(target.x, target.y));
        path = pf.path;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= (moveFrequency * aiCollective.speed)) {
            if (!unit.isCombatMoving) {
                if (unit.remainingActions <= 0) {
                    Debug.Log("AI tried to use action to move but couldn't.");
                    unit.SetAIState(AIState.idle, AIState.idle);
                    return;
                }
                unit.startMove();
            } else {
                if (path.Count > 1) {
                    path.RemoveAt(path.Count-1);
                    Vector2Int nextPath = path[path.Count-1];
                    moveTarget = new Vector3Int(nextPath.x, nextPath.y, 0) - unit.gridPosition;
                    unit.moveTo(moveTarget);

                    //if ran out of movement points
                    if (!unit.isCombatMoving) {
                        unit.SetAIState(AIState.idle, AIState.idle);
                    }
                } else {
                    unit.endMove();
                    unit.SetAIState(AIState.idle, AIState.idle);
                }
            }
            timeSinceLastMove = 0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.isCombatMoving) unit.endMove();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
