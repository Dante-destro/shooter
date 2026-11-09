using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Animator enemyAnimator;
    public bool isDead = false;

    [Header("Configuración de Persecución")]
    public float updateTargetInterval = 0.3f; // Cada cuánto tiempo el Invoke refresca tu posición

    [Header("Tiempos de Muerte")]
    public float deathAnimationDuration = 5.2f; // Duración exacta de la animación de muerte

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }

        enemyAnimator = GetComponentInChildren<Animator>();

        // Iniciamos una corrutina muy corta solo para asegurar la colocación en el NavMesh
        StartCoroutine(InitializeAgent());
    }

    IEnumerator InitializeAgent()
    {
        // Espera un frame para evitar errores de NavMeshAgent al aparecer
        yield return null;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = false;
            // Arrancamos el InvokeRepeating para que empiece a perseguirte continuamente
            InvokeRepeating("UpdateTarget", 0f, updateTargetInterval);
        }
    }

    // Método que ejecuta el Invoke periódicamente para actualizar la ruta
    void UpdateTarget()
    {
        if (isDead || target == null || agent == null || !agent.isOnNavMesh) return;

        agent.SetDestination(target.position);
    }

    void Update()
    {
        if (isDead) return;

        // Control de la animación de movimiento ("IsMoving" con I mayúscula)
        if (agent != null && agent.isOnNavMesh && agent.desiredVelocity.magnitude > 0.1f && !agent.isStopped)
        {
            enemyAnimator.SetBool("IsMoving", true);
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", false);
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Cancelamos el Invoke de inmediato para que deje de buscar destino
        CancelInvoke("UpdateTarget");

        Debug.Log("Enemy has died!");

        // Apagamos el movimiento
        enemyAnimator.SetBool("IsMoving", false);

        // Activamos la animación de muerte
        enemyAnimator.SetTrigger("Die");

        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // Desactivar el colisionador
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Iniciamos la secuencia de muerte de 5.2 segundos
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Espera los 5.2 segundos que dura la animación de muerte
        yield return new WaitForSeconds(deathAnimationDuration);

        // Sumar la baja al PlayerManager (esto activará la victoria si es el último)
        PlayerManager player = FindObjectOfType<PlayerManager>();
        if (player != null)
        {
            player.AddDefeatedEnemy();
        }

        // Destruir el objeto de la escena
        Destroy(gameObject);
    }
}