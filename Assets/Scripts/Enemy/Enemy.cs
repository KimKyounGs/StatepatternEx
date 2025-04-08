using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 정보")]
    public float moveSpeed = 5f;
    public float attackDamage = 10;

    public int facingDir { get; private set; } = 1; // 1: 오른쪽, -1: 왼쪽, 기본값 1(오른쪽)
    private bool facingRight = true; // 기본값 오른쪽


    [Header("충돌 정보")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsWall;


    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    public EnemyStateMachine StateMachine; // 상태 머신

    public EnemyIdleState idleState { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, StateMachine, "Idle");   
    }

    private void Start()
    {
        StateMachine.Initialize(idleState); // 초기 상태 설정
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }


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
}
