using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Player : MonoBehaviour
{
    [Header("공격 디테일")]
    public Vector2[] attackMovement = new Vector2[3]; // 공격 시 이동 방향
    public bool isBusy { get; private set; }
    [Header("플레이어 정보")] 
    public float moveSpeed = 12f;
    public float jumpForce;
    public float wallSlideSpeed;
    public float wallJumpForce;

    [Header("대시 정보")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    

    [Header("충돌 정보")]
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    public int facingDir { get; private set; } = 1; // 1: 오른쪽, -1: 왼쪽, 기본값 1(오른쪽)
    private bool facingRight = true; // 기본값 오른쪽

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion
    

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }


    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public void ZeroVeclocity() => rb.linearVelocity = new Vector2(0, 0);
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

       
        yield return new WaitForSeconds(_seconds);
      
        isBusy = false;
    }


    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer<0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
           
    }

    #region 충돌
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    #endregion

    #region 플립
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); // 180도 회전
    }

    public void FlipController(float velocity)
    {
        if (velocity> 0 && !facingRight) // 오른쪽으로 가고 있는데 왼쪽을 바라보고 있다면
        {
            Flip();
        }
        else if (velocity < 0 && facingRight) // 왼쪽으로 가고 있는데 오른쪽을 바라보고 있다면
        {
            Flip();
        }
    }
    #endregion
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger(); // 애니메이션 트리거 호출
    

}
