using UnityEngine;
using UnityEngine.AI;

public class enemiNavigator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NavMeshAgent agent;
    public Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    
       
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("UpdateTarget", 3f, 5f);
    }

    private void UpdateTarget()
    {
        agent.destination = target.position; // Update the agent's destination to the target's position every frame
    }
     }
