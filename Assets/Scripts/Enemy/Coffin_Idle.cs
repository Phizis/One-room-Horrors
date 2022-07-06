using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin_Idle : StateMachineBehaviour
{
    float timer;
    Transform player;
    float chaseRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        chaseRange = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(chaseRange < 8)
            chaseRange += Time.deltaTime * 0.7f;
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);

        timer += Time.deltaTime;
        if (timer > 3 && distance > chaseRange)
            animator.SetBool("isPatroling", true);
    }    
}
