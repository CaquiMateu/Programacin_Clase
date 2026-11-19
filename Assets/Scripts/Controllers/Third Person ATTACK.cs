using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonATTACK : MonoBehaviour
{

    [SerializeField] private int currentCombo = 0;
    [SerializeField]private int MaxAttackCombo = 3;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        currentCombo++;

        anim.SetInteger("AttackID", currentCombo);

        if (currentCombo >= MaxAttackCombo) 
        { 
        
            currentCombo = 0;
        
        }
    }
}
