using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Animator enemyAnimator;
    public bool isDead = false;

    [Header("Tiempo de Espera")]
    public float chaseDelay = 3f;      // Tiempo en segundos que espera antes de empezar a perseguir
    private bool canChase = false;     // Controla si ya puede empezar a moverse

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            InvokeRepeating("UpdateTarget", 3f, 2f);
        }

        enemyAnimator = GetComponentInChildren<Animator>();

        // Iniciamos la corrutina de espera antes de perseguir
        StartCoroutine(WaitBeforeChase());
    }

    IEnumerator WaitBeforeChase()
    {
        // Aseguramos que el agente esté detenido al inicio
        agent.isStopped = true;

        // Espera los segundos configurados en 'chaseDelay'
        yield return new WaitForSeconds(chaseDelay);

        // Pasado el tiempo, permitimos que empiece la persecución
        canChase = true;
        agent.isStopped = false;
    }

    void Update()
    {
        // Si está muerto, no hacemos nada
        if (isDead) return;

        // Solo persigue si ya pasó el tiempo de espera
        if (canChase && target != null)
        {
            agent.SetDestination(target.position);

            // Control de la animación de movimiento
            if (agent.desiredVelocity.magnitude > 0.1f && !agent.isStopped)
            {
                enemyAnimator.SetBool("IsMoving", true);
            }
            else
            {
                enemyAnimator.SetBool("IsMoving", false);
            }
        }
        else
        {
            // Mientras espera, se asegura de mantener la animación de Idle
            enemyAnimator.SetBool("IsMoving", false);
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Enemy has died!");

        // Apagamos el movimiento
        enemyAnimator.SetBool("IsMoving", false);

        // Activamos la animación de muerte
        enemyAnimator.SetTrigger("Die");

        agent.isStopped = true;
        agent.enabled = false;

        // Desactivar el colisionador
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Iniciamos la secuencia de espera antes de sumar la victoria y destruir
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Espera a que termine la animación de muerte
        yield return new WaitForSeconds(2f);

        // Sumar la baja al PlayerManager (esto activará la victoria si es el último)
        PlayerManager player = FindObjectOfType<PlayerManager>();
        if (player != null)
        {
            player.AddDefeatedEnemy();
        }

        // Destruir el objeto
        Destroy(gameObject);
    }
}