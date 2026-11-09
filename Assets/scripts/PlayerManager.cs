using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public Image healthBar;

    [Header("Contador de Enemigos y Victoria")]
    public int enemiesToWin = 5;         // Cantidad de enemigos necesarios para ganar
    private int defeatedEnemies = 0;     // Enemigos actuales derrotados
    public TextMeshProUGUI enemiesText;  // Texto en UI para mostrar el contador (ej. "Enemigos: 0/5")

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    // Recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerDefeat();
            }
            else
            {
                Debug.LogWarning("GameManager.Instance no encontrado en la escena.");
            }
        }
    }

    // Método para registrar enemigos derrotados (llamado desde el enemigo o proyectil)
    public void AddDefeatedEnemy()
    {
        defeatedEnemies++;
        UpdateUI();

        Debug.Log("Enemigo derrotado. Total: " + defeatedEnemies);

        // Condición de victoria basada en el contador de enemigos
        if (defeatedEnemies >= enemiesToWin)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerVictory();
            }
            else
            {
                Debug.LogWarning("GameManager.Instance no encontrado en la escena.");
            }
        }
    }

    void UpdateUI()
    {
        // Actualizar vida
        if (healthText != null) healthText.text = currentHealth.ToString();
        if (healthBar != null) healthBar.fillAmount = (float)currentHealth / maxHealth;

        // Actualizar contador de enemigos en la UI
        if (enemiesText != null)
        {
            enemiesText.text = "Enemigos: " + defeatedEnemies + " / " + enemiesToWin;
        }
    }

    // Detectar si el enemigo colisiona con el jugador para hacerle daño
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(10); // Daño que recibe el jugador al tocar al enemigo
            }
        }
    }
}