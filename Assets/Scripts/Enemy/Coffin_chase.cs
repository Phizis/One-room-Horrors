using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Coffin_chase : StateMachineBehaviour
{
    NavMeshAgent enemyAgent;
    Transform player;
    float chaseRange = 8;
    float chaseTimer;
    public float enemySpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent = animator.GetComponent<NavMeshAgent>();
        enemyAgent.speed = 4;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        chaseTimer = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        chaseTimer+= Time.deltaTime;
        if( distance > chaseRange || chaseTimer > 8)
            animator.SetBool("isChasing", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.SetDestination(enemyAgent.transform.position);
        enemyAgent.speed = enemySpeed;
    }
}
