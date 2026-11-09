using UnityEngine;
using TMPro;

public class receiveDamage : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public int health = 100;

    public enemyNavigation enemyNavigation;

    [Header("Animación")]
    public Animator animator; // Referencia al Animator del enemigo

    void Start()
    {
        enemyNavigation = GetComponent<enemyNavigation>();

        // Si no asignaste el Animator en el inspector, lo busca automáticamente en el objeto
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Received damage from projectile!");

            Destroy(collision.gameObject);

            health -= 25;

            // Protección por si healthText es nulo en algún momento
            if (healthText != null)
            {
                healthText.text = health.ToString();
            }

            if (health <= 0)
            {
                Debug.Log("Enemy has died!");

                // 1. Activar la animación de muerte si el Animator existe
                if (animator != null)
                {
                    animator.SetTrigger("Die"); // Asegúrate de tener un Trigger llamado "Die" en tu Animator
                }

                // 2. Llamar al método de muerte del navegador/enemigo
                if (enemyNavigation != null)
                {
                    enemyNavigation.Die();
                }
            }
        }
    }
}