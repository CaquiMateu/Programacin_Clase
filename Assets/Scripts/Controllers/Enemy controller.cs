using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.ComponentModel.Design.Serialization;
using System;

public class Enemycontroller : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject player;

    [Header("Detection")]
    [Range(0, 360)]
    [SerializeField] private float DetectionRange = 5;
    [SerializeField] private float DetectionAngle;
    [SerializeField] private LayerMask layerDetecor;
    [SerializeField] private Transform detectedTarget;

    private Transform chaseTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        CheckvisionRange();

        if(chaseTarget != null)
        {
            agent.SetDestination(chaseTarget.position);
        }
    }
    void Detect()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, DetectionRange, layerDetecor);
        if (detectedObjects.Length > 0)
        {
            if (detectedTarget == null)
            {
                detectedTarget = detectedObjects[0].transform;
            }
        }
        else if (detectedTarget != null) 
        {
            detectedTarget = null;
        
        }

    }

    void CheckvisionRange()
    {
        if (detectedTarget == null || chaseTarget != null) 
        {
            return;
        }

        // Calcular la dirección hacia el objeto

        Vector3 direction = detectedTarget.position - transform.position;
        // Calcular el producto escalar entre la dirección en la que mira y la dirección del jugador

        float dot = Vector3.Dot(transform.forward, direction);

        if (dot >= Mathf.Cos(DetectionAngle * 0.5f * Mathf.Deg2Rad))
        {
            chaseTarget = detectedTarget;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
