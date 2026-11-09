using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceiveDamage : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public int health = 100;

    public EnemyNavigation enemyNavigation;

    void Start()
    {
        enemyNavigation = GetComponent<EnemyNavigation>();
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
            healthText.text = health.ToString();

            if (health <= 0)
            {
                Debug.Log("Enemy has died!");
                enemyNavigation.Die();
            }
        }
    }

}