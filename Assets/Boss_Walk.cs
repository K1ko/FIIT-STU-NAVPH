using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed;
    public float attackRange = 10f; // Boss stops walking and starts attacking at this range

    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        Debug.Log("Boss Walk State Entered");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        float distanceToPlayer = Vector2.Distance(player.position, rb.position);

        if (distanceToPlayer >= attackRange)
        {
            // Move toward the player
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            Debug.Log("Boss moving toward player at: " + target);
        }
        else if (distanceToPlayer <= attackRange)
        {
            // Stop and attack
            animator.SetTrigger("Attack");
            Debug.Log("Boss in range (" + distanceToPlayer + "), triggering Attack.");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}

