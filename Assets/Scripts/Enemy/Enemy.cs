using UnityEngine;

public class Enemy : Entity
{
    [Header("이동 정보")]
    public float moveSpeed;
    public float idleTime;

    public EnemyStateMachine StateMachine; // 상태 머신

    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update(); // 현재 상태 업데이트
    }


}