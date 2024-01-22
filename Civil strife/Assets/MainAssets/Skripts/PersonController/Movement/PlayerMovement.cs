using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Other Scripts")]
    //public Dash dash;
    public WallSliding wall;

    [SerializeField] private Settings set;

    [Header("Movement")]
    public bool canRun = true;
    public bool canFlip = true;
    public bool canJump = true;

    public float velocityY;

    private int frontOfDirection = 1;

    [SerializeField] private float movementSpeed;
    private float attackMovementSpeed;
    private float normalMovementSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float heightOfJumpMultiplier;
    private float movementDirection;

    [SerializeField] private Rigidbody2D rb;// rb ������

    [Header("Effects")]
    [SerializeField] private ParticleSystem jumpTrail;

    [SerializeField] private AudioSource stepAud;
    [SerializeField] private AudioSource jumpAud;
    [SerializeField] private AudioSource dashAud;

    [Space]

    [Header("Player Animator")]
    public bool isRight;
    private bool isWalking;

    [SerializeField] private Animator an;

    [SerializeField] private Transform drawablePart; //�������������� ����� ������

    [Space]

    [Header("Collision")]
    private bool isGround;

    [SerializeField] private float grRad;//������ groundCheck

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whichGround;
    private void Start()
    {
        attackMovementSpeed = movementSpeed / 3;
        normalMovementSpeed = movementSpeed;
    }
    private void Update()
    {
        InputCheck();

        MovementDirectionCheck();

        AnimatorUpdate();
    }

    private void FixedUpdate()
    {
        Movement();

        CheckCollision();
    }

    private void InputCheck()
    {
        MovementInput();

        JumpInput();
    }

    //������ ��� ���������� ��������
    #region Movement
    private void MovementInput()
    {
        if (canRun)
        {
            movementDirection = Input.GetAxisRaw("Horizontal");
        }
    }

    private void MovementDirectionCheck()
    {
        if (canFlip)
        {
            if (isRight && movementDirection < 0 && canRun)
                Flip();
            else if (!isRight && movementDirection > 0 && canRun)
                Flip();
        }

        if ((rb.velocity.x > 0.05f || rb.velocity.x < -0.05f) && !isWalking)
        {
            isWalking = true;
            stepAud.Play();
        }
        else if ((rb.velocity.x < 0.05f && rb.velocity.x > -0.05f) && isWalking)
        {
            isWalking = false;
            stepAud.Stop();
        }

        if (!isGround)
        {
            stepAud.Stop();
            isWalking = false;
        }
    }

    private void Movement()
    {
        velocityY = rb.velocity.y;

        if (canRun)
        {
            rb.velocity = new Vector2(movementSpeed * movementDirection, rb.velocity.y);
        }
        //if (Input.GetButtonUp("Jump")) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * heightOfJumpMultiplier);
    }
    #endregion

    //������ ��� ���������� �������� � ����������� ���������
    #region Animation
    private void AnimatorUpdate()
    {
        an.SetBool("IsWalking", isWalking);
        an.SetBool("isGround", isGround);
    }

    public void Flip()
    {
        isRight = !isRight;
        frontOfDirection = -frontOfDirection;
        drawablePart.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    //������ ��� ���������� ���� ����� ������� 
    #region Jump
    private void CheckCollision()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, grRad, whichGround);
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(set.jumpAction) && isGround && canRun && canJump) 
        {
            jumpAud.Play();

            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            StartJumpTrail();
        }
    }
    #endregion

    //��������� ���������� ��� ������
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, grRad);
    }

    //������� � �������
    #region Getters/Setter or Enabled/Disabled Functions
    public float plDirection()
    {
        if (movementDirection == 0)
        {
            return frontOfDirection;
        }

        return movementDirection;
    }
    public int plFront()
    {
        return frontOfDirection;
    }
    public bool plGround()
    {
        return isGround;
    }
    public void StopPlayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        canRun = false;
        canJump = false;
        canFlip = false;
        isWalking = false;
        stepAud.Stop();
        an.SetBool("IsWalking", isWalking);
    }
    public void SlowPlayer()
    {
        canJump = false;
        canFlip = false;
        movementSpeed = attackMovementSpeed;
    }
    public void FastPlayer()
    {
        canJump = true;
        canFlip = true;
        movementSpeed = normalMovementSpeed;
    }
    public void UnFreezePlayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        canRun = true;
        canJump = true;
        canFlip = true;
    }
    public void Sitting(bool flag)
    {
        canFlip = false;
        canRun = false;
        rb.velocity = new Vector2(0f, 0f);
        an.SetBool("isSitting", flag);
    }
    #endregion

    #region Effects
    public void StartJumpTrail()
    {
        jumpTrail.Play();
    }

    public void StopJumpTrail()
    {
        //jumpTrail.enabled = false;
    }
    #endregion
}
