using UnityEngine;

public class spawnerTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject objectToSpawn; // Assign the prefab in the Inspector

    public Vector3 spawnposition; // Position where the object will be spawned
    void Start()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
