using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Necesario para usar corrutinas

public class enemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Animator enemyAnimator;
    public bool isDead = false;

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
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (agent.velocity.magnitude > 0.1f)
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
        if (target != null && !isDead)
        {
            agent.SetDestination(target.position);
        }
    }

    public void Die()
    {
        if (isDead) return; // Evita que se ejecute varias veces
        isDead = true;

        Debug.Log("Enemy has died!");

        // 1. Activar animación y detener el movimiento
        enemyAnimator.SetTrigger("Die");
        agent.isStopped = true;
        agent.enabled = false;

        // 2. Desactivar el colisionador para que no moleste
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 3. Iniciar la corrutina que espera a que termine la animación antes de sumar la victoria y destruirse
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Espera el tiempo exacto que dura tu animación de muerte (ej. 2 segundos)
        // Cambia este número por lo que dure tu animación real en segundos
        yield return new WaitForSeconds(5f);

        // 4. DESPUÉS de la animación, sumamos la baja al PlayerManager (esto activará la victoria si es el último)
        PlayerManager player = FindObjectOfType<PlayerManager>();
        if (player != null)
        {
            player.AddDefeatedEnemy();
        }

        // 5. Destruir el objeto
        Destroy(gameObject);
    }
}