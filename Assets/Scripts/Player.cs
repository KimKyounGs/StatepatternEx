using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("이동 정보")]
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘

    #region Components
    public Animator anim { get; private set; }

    public Rigidbody2D rb { get; private set; }
    #endregion


    #region State
    // 플레이어의 상태를 관리하는 상태머신
    public PlayerStateMachine stateMachine { get; private set; }

    // 플레이어의 상태 (대기 상태, 이동 상태)
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    #endregion

    private void Awake()
    {
        // 상태 머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();

        // 각 상태 인스턴스 생성 (this는 현재 Player 인스턴스를 참조, stateMachine은 상태 머신 인스턴스를 참조, "Idle"과 "Move"는 상태의 이름)
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");    
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Air");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // 게임 시작시 대기 상태로 초기화
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }


}
