using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack_Behaviour : StateMachineBehaviour {
    CREnemy unit;
    AICollective aiCollective;
    CRUnit target = null;
    float timeSinceLastMove = 0f;
    const float moveFrequency = 1.5f;
    bool targetSelected = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Game game = FindObjectOfType<Game>();
        unit = animator.transform.parent.gameObject.GetComponent<CREnemy>();
        aiCollective = FindObjectOfType<AICollective>();

        //Find closest player distance
        float closestPlayerDistance = Mathf.Infinity;
        target = null;
        foreach (CRPlayer player in game.players) {
            float playerDistance = Util.GridDistance(unit.gridPosition-player.gridPosition);
            if ( !player.isDead && playerDistance < closestPlayerDistance) {
                closestPlayerDistance = playerDistance;
                target = player;
            }
        }

        //Can use Skill?
        unit.aiStateSkill = null;
        foreach (Skill skill in unit.skills) {
            if (skill.type == targetType.Direct) {
                if (unit.CanCast(skill) && closestPlayerDistance <= (skill.range + skill.radius)) {
                    unit.aiStateSkill = skill;
                }
            } else if (skill.type == targetType.Self) {
                if (unit.CanCast(skill) && closestPlayerDistance <= (skill.radius)) {
                    unit.aiStateSkill = skill;
                }
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (unit.aiStateSkill == null && Util.GridDistance(unit.gridPosition-target.gridPosition) > unit.BasicAttackRange) {
            unit.SetAIState(AIState.move, AIState.attack);
            return;
        }

        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= (moveFrequency * aiCollective.speed)) {
            if (!target || target.isDead) {
                unit.SetAIState(AIState.idle, AIState.idle);
                return;
            }

            if (!unit.isTargeting) {
                targetSelected = false;
                if (unit.aiStateSkill == null) {
                    unit.StartBasicAttack();
                } else {
                    unit.targetSkill = unit.aiStateSkill;
                    unit.startTargeting(unit.aiStateSkill.targetDisplay);
                }
            } else if (!targetSelected) {
                // TODO: Fix radius abilities
                unit.selection.Move(target.gridPosition - unit.gridPosition);
                targetSelected = true;
            } else {
                unit.ExecuteAttack();
                unit.SetAIState(AIState.idle, AIState.idle);
            }
            timeSinceLastMove = 0f;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
