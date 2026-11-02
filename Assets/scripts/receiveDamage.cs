using UnityEngine;

public class recive : MonoBehaviour
{
    public int health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Received damage from projectile");
            Destroy(collision.gameObject);
            health -= 10;
            Debug.Log("Health: " + health);
            if (health <= 0)
            {
                Debug.Log("Object destroyed");
                Destroy(gameObject);
            }
        }
    }
}
