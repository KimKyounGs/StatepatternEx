using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region State
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    #endregion

    
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, StateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState); // 초기 상태를 idleState로 설정
    }

    protected override void Update()
    {
        base.Update();
    }
}
