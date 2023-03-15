using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public delegate void OnSeePlayer();

    public event OnSeePlayer onSeePlayer;

    [SerializeField] protected AudioSource[] enemAudio;


    [Header("ИИ")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform _transform;
    [SerializeField] private Transform enemyEye;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float patrolArea;
    [SerializeField] private float speed;
    [SerializeField] private float pursuitSpeed;
    [SerializeField] private float delayBeforeRotation;
    [SerializeField] private float seeDistance;
    [SerializeField] private float seeDistanceBack;

    [SerializeField] private string playerTag;
    [SerializeField] private string objTag;

    [SerializeField] private float stoppingDistance = 2;
    private float currentPoint;

    public int currentDirection = 1;

    [SerializeField] private bool canMoving = true;
    private bool canAttack;
    private bool seePlayer;
    private bool playerOnBack = true;

    [Header("Анимации")]
    [SerializeField] protected Animator enemyAnim;

    [SerializeField] protected ParticleSystem particle;

    [Header("Сиситема атаки")]
    [SerializeField] private Transform attackHitBoxPos;

    [SerializeField] private float attackRad;
    [SerializeField] private float damage;
    [SerializeField] private float speedAttack;

    protected bool isAttacking;

    private PlayerMovement plMove;

    [Header("Система здоровья")]
    [SerializeField] private Transform healthFieldDrawable;

    [SerializeField] private GameObject canv;

    [SerializeField] private float health;
    private float healthMax;

    private bool isHearting;
    public bool isAlive = true;

    [Header("Отталкивание")]
    [SerializeField] private float knockSpeedX;
    [SerializeField] private float knockSpeedY = 1f;

    [Header("Рывок")]
    [SerializeField] private bool isDashing = false;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashMaxTime;
    [SerializeField] private float dashTimeToReboot;
    [SerializeField] private float dashRebTimer;
    [SerializeField] private float dashTimer;

    [SerializeField] protected bool canDash;

    protected virtual void Start()
    {
        enemyEye.position = new Vector3(_transform.position.x - seeDistanceBack,
            _transform.position.y, _transform.position.z);

        currentPoint = gameObject.transform.position.x + patrolArea / 2 * currentDirection;

        player = GameObject.FindGameObjectWithTag(playerTag).transform;

        plMove = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();

        healthMax = health;

        groundCheck.parent = null;
    }

    protected virtual void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (!seePlayer) Patroll();
        else if (isDashing) Dash();
        else PursuitPlayer();

        CalmPain();
        if (seePlayer) DashReboot();
    }

    #region PursuitPlayer

    private void PursuitPlayer()
    {
        CheckForRotation();

        var _collider = GetRaycastHit(stoppingDistance).collider;
        if (_collider == null)
        {
            ChangePermissionCanMoveOrNot(true);
        }
        else if (_collider.CompareTag(playerTag) || (seePlayer &&_collider.CompareTag(objTag)))
        {
            if (canAttack)
                ChangePermissionCanMoveOrNot(false);
                StartAttack();
        }

        Move(GetSpeed(pursuitSpeed));
    }

   

    private void CheckForRotation()
    {
        var _playerPosX = player.position.x;
        var _enemyPosX = _transform.position.x;

        if (((_playerPosX < _enemyPosX && currentDirection == 1) || 
            (_playerPosX > _enemyPosX && currentDirection == -1)) && playerOnBack)
        {
            playerOnBack = false;
            Invoke("CheckRotationFight", delayBeforeRotation);
        }
    }

    private void CheckRotationFight()
    {
        var _playerPosX = player.position.x;
        var _enemyPosX = _transform.position.x;


        if (((_playerPosX < _enemyPosX && currentDirection == 1) ||
            (_playerPosX > _enemyPosX && currentDirection == -1)))
        {
            playerOnBack = true;

            ChangeDirection();
            ChangeRotation();
        }
    }
   

    private void CheckForPlayer()
    {
        if (GetRaycastHit(seeDistance + seeDistanceBack).collider != null)
        {
            seePlayer = true;

            onSeePlayer?.Invoke();
        }
    }

    #endregion

    #region Patroll

    private void Patroll()
    {
        var _currentPositionX = _transform.position.x;

        CheckForPlayer();

        Move(GetSpeed(speed));

        if (_currentPositionX > currentPoint - 0.2 && _currentPositionX < currentPoint + 0.2)
            ChangePoint();
    }

    public void ChangePoint()
    {
        currentPoint += patrolArea * (-currentDirection);

        ChangePermissionCanMoveOrNot(false);

        Invoke("ChangeDirection", delayBeforeRotation);

        Invoke("ChangeRotation", delayBeforeRotation);
    }

    public void ChangeDirection()
    {
        currentDirection *= -1;
    }

    public void ChangeRotation()
    {
        gameObject.transform.Rotate(0, gameObject.transform.rotation.y + 180 * currentDirection, 0);

        if (!seePlayer)
            ChangePermissionCanMoveOrNot(true);
    }

    #endregion

    #region GetOrSetSomeParameters

    private RaycastHit2D GetRaycastHit(float _distance)
    {
        return Physics2D.Raycast(enemyEye.position, new Vector3(180 * currentDirection, 0, 0),
            _distance, layerMask);
    }

    private float GetSpeed(float _speed)
    {
        if (canMoving)
            return _speed;
        return 0;
    }

    protected bool GetCanAttack()
    {
        return canAttack;
    }

    public void ChangePermissionCanMoveOrNot(bool _canMove)
    {
        canMoving = _canMove;
    }

    public bool GetSeePlayer()
    {
        return seePlayer;
    }

    public void ChangeCanAttack(bool _can)
    {
        canAttack = _can;
    }

    public void ChangeSeePlayer(bool _sawPlayer)
    {
        seePlayer = _sawPlayer;
    }

    public void ChangeStoppingDistance(float _stopDist)
    {
        stoppingDistance = _stopDist;
    }

    public void SetHealth(float _damage)
    {
        health -= _damage;
    }
    public float GetHealth()
    {
        return health;
    }
    
    public void ChangeCanDashOrNot(bool can)
    {
        canDash = can;
    }
    public float GetTimeToReboot()
    {
        return dashTimeToReboot;
    }
    public float GetRebootTimer()
    {
        return dashRebTimer;
    }

    #endregion

    #region Dash

    private void Dash()
    {
        if (isDashing)
        {
            Move(dashSpeed);
            dashTimer += Time.deltaTime;
        }
        if (dashTimer >= dashMaxTime)
        {
            StopDash();
        }

    }
    private void StartDash()
    {
        ChangeCanAttack(false);
        //ChangePermissionCanMoveOrNot(false);
        isDashing = true;
        canDash = false;
        dashRebTimer = 0;
    }

    private void StopDash()
    {
        ChangeCanAttack(true);
        //ChangePermissionCanMoveOrNot(true);
        isDashing = false;
        dashTimer = 0;
        canDash = true;
    }

    private void DashReboot()
    {
        if (canDash)
        {
            dashRebTimer += Time.deltaTime;
        }
        if (dashRebTimer >= dashTimeToReboot)
        {
            StartDash();
        }
    }

    #endregion

    protected void Move(float _speed)
    {
        rb.velocity = new Vector2(_speed * currentDirection,rb.velocity.y);
        //Debug.Log(_rb.velocity.y);
    }  

    #region AttackSistem
    private void CheckAttackHitBox()
    {
        Collider2D detectedObj = Physics2D.OverlapCircle(attackHitBoxPos.position, 
            attackRad, layerMask);
        detectedObj.transform.SendMessage("Damage", damage*currentDirection);

    }

    private void StartAttack()
    {
        enemAudio[0].Play();

        //Move(0);
        isAttacking = true;
        enemyAnim.SetBool("isAttacking", isAttacking);
    }
    protected virtual void EndAttack()
    {
        isAttacking = false;
        enemyAnim.SetBool("isAttacking", isAttacking);
        ChangePermissionCanMoveOrNot(true);
    }


    #endregion

    #region HealthSistem

    private void CalmPain()
    {
        healthFieldDrawable.localScale = new Vector3(health / healthMax, 1, 1);
        if (isHearting) rb.AddRelativeForce(new Vector2(knockSpeedX, 0f));
    }
    protected virtual void Damage(float damage)
    {
        particle.Play();

        rb.AddRelativeForce(new Vector2(0f, knockSpeedY));
        if (plMove.isRight) knockSpeedX = Mathf.Abs(knockSpeedX);
        else knockSpeedX = -1 * Mathf.Abs(knockSpeedX);

        if (health > damage)
        {
            enemAudio[1].Play();

            health -= damage;

            isHearting = true;
            enemyAnim.SetBool("isDamage", isHearting);

            ChangePermissionCanMoveOrNot(false);
            
        }
        else
        {
            health = 0f;

            Death();
        }
    }

    private void EndPain()
    {
        isHearting = false;
        enemyAnim.SetBool("isDamage", isHearting);

        ChangePermissionCanMoveOrNot(true);
    }

    protected virtual void Death()
    {
        isAlive = false;

        enemAudio[2].Play();

        enemyAnim.SetBool("isDead", true);

        canv.SetActive(false);

        rb.velocity = new Vector2(0f, rb.velocity.y);
        gameObject.layer = 10;
    
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRad);
        Gizmos.DrawWireSphere(new Vector2(groundCheck.position.x + patrolArea/2, 
            groundCheck.position.y), 0.3f);
        Gizmos.DrawWireSphere(new Vector2(groundCheck.position.x - patrolArea / 2,
            groundCheck.position.y), 0.3f);
    }
}
