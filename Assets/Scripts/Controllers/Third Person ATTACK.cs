using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonATTACK : MonoBehaviour
{

    [SerializeField] private int currentCombo = 0;
    [SerializeField] private int MaxAttackCombo = 3;
    [SerializeField] private bool canCombo = false;
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
            Debug.Log("Hola");

            if (currentCombo == 0)
            {
                Attack();
            }
            else if (canCombo == true)
            {
                ComboAttack();
            }

        }
    }

    void Attack()
    {

        currentCombo++;

        anim.SetInteger("AttackID", currentCombo);

        //if (currentCombo >= MaxAttackCombo) 
        //{ 

        //    currentCombo = 0;

        //}
    }

    void ComboAttack()
    {
        canCombo = false;

        currentCombo++;

        anim.SetInteger("AttackID", currentCombo);
    }

    public void EnableCombo()
    {
        canCombo= true;
    }

    public void EndOfAttack()
    {
        canCombo = false;
        currentCombo = 0;
        anim.SetInteger("AttackID", 0);
    }
}

