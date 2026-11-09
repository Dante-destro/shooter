using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public Animator enemyAnimator;

    public bool isDead = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("UpdateTarget", 3f, 2f);

        enemyAnimator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (isDead)
        {
            agent.isStopped = true;
            return;
        }

        if (agent.velocity.magnitude > 0.1)
        {
            enemyAnimator.SetBool("isMoving", true);
        }
        else
        {
            enemyAnimator.SetBool("isMoving", false);
        }
         
    }

    private void UpdateTarget()
    {
       agent.SetDestination(target.position);

    }
    public void Die()
    {
        Debug.Log("Enemy has died!");
        enemyAnimator.SetTrigger("Die");
        agent.isStopped = true;
        isDead = true;
    }

}