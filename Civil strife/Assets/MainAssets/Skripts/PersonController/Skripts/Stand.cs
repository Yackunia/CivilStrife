using UnityEngine;

public class Stand : MonoBehaviour
{
    public bool canAttack = true;

    private bool isRight;

    [SerializeField] private bool isFirstAttack = true;
    [SerializeField] private bool canCombo = true;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool needToTake = false;

    [SerializeField] private float attackRad;
    [SerializeField] private float attackDamage;

    [SerializeField] private Transform[] atHitBoxPosition;

    [SerializeField] private LayerMask isDamageable;

    [SerializeField] private Animator anim;

    private PlayerMovement move;

    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        anim.SetBool("attack", isAttacking);
        move.canFlip = !isAttacking;

        if (Input.GetButtonDown("Fire1") && canAttack && isFirstAttack) 
        {
            isFirstAttack = false;
            isAttacking = true;
            canCombo = true;
        }
        if (Input.GetButtonDown("Fire1") && canAttack && !isFirstAttack && !canCombo)
        {
            canCombo = true;
        }
        if (Input.GetButtonUp("Fire1") && canAttack && !isFirstAttack && canCombo && needToTake)
        {
            EndAttack();
        }
    }

    private void StartCombo()
    {
        canCombo = false;
    }
    private void CheckCombo()
    {
        isAttacking = canCombo;
        canCombo = true;
        if (isAttacking == false) EndAttack();
    }

    private void StartTake()
    {
        needToTake = true;
        canCombo = false;
    }

    private void EndTake()
    {
        needToTake = false;
    }

    private void EndAttack()
    {
        isAttacking = false;
        isFirstAttack = true;
        canCombo = true;
        needToTake = false;
    }

    private void AttackHitBox0()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(atHitBoxPosition[0].position, attackRad, isDamageable);

        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("Damage", attackDamage);
        }
    }
    private void AttackHitBox1()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(atHitBoxPosition[1].position, attackRad, isDamageable);

        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("Damage", attackDamage);
        }
    }
}
