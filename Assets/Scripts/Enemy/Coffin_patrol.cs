using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Coffin_patrol : StateMachineBehaviour
{
    float timer;
    List<Transform> patrolPoints = new List<Transform>();
    NavMeshAgent enemyAgent;

    Transform player;
    float chaseRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        chaseRange = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("PatrolPath").transform;
        foreach (Transform point in pointsObject)
            patrolPoints.Add(point);
        enemyAgent = animator.GetComponent<NavMeshAgent>();
        enemyAgent.SetDestination(patrolPoints[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            enemyAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Count)].position);
        }

        if(chaseRange < 8)
            chaseRange+= Time.deltaTime*0.7f;

        timer += Time.deltaTime;
        if(timer > 10)
        {
            animator.SetBool("isPatroling", false);
        }

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.SetDestination(enemyAgent.transform.position);
    }
}
