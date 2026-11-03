using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class Enemycontroller : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
