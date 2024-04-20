using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float health = 100;
    public AudioSource DieSnd, attackSnd;
    public Transform playerTarget;
    public float chasingDistance = 15f; // Distance at which zombie starts chasing

    float attackCoolDownTimer;
    bool isDead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isDead)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer <= chasingDistance)
            {
                agent.SetDestination(playerTarget.position);
                animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

                if (agent.velocity.sqrMagnitude <= 0 && attackCoolDownTimer <= 0)
                {
                    animator.SetTrigger("Attack");
                    // Damage player
                    // attackSnd.Play();
                    attackCoolDownTimer = 2f;
                }

                if (attackCoolDownTimer > 0)
                    attackCoolDownTimer -= Time.deltaTime;
            }
            else
            {
                // Zombie is too far from player, stop moving
                agent.velocity = Vector3.zero;
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    public void TakeDamage()
    {
        if (isDead)
            return;

        health -= 20;
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
            return;

        agent.isStopped = true;
        isDead = true;
        animator.SetTrigger("Dead");
        DieSnd.Play();
    }
}
