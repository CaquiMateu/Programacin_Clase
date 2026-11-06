using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.ComponentModel.Design.Serialization;
using System;
using System.Collections;

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

    [Header("Combat")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackSpeed;
    private bool CanAttack = true;
    [SerializeField] private float lookAtSpeed;

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

            if (agent.remainingDistance <= attackDistance)
            {
                LookAtPlayer();
                if (CanAttack == true)
                {
                    Attack();
                }
               
            }
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
    void Attack()
    {
        StartCoroutine(AttackCrt());
    }

    IEnumerator AttackCrt()
    {
        CanAttack = false;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(attackSpeed - attackTime);
        CanAttack = true;
    }

    void LookAtPlayer()
    {
        Vector3 DirectionToTarget = chaseTarget.position - transform.position;
        DirectionToTarget.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DirectionToTarget), Time.deltaTime * lookAtSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        Gizmos.color = Color.burlywood;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        if(agent != null)
        {
            Gizmos.color = Color.rebeccaPurple;
            Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
        }
       
    }
}
