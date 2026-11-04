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
        agent.destination= target.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
