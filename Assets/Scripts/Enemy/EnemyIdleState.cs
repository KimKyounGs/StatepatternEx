using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class EnemyIdleState : EnemyState
{
    private Enemy enemy;
    private float idleTime = 2f; // 대기 시간
    private float idleTimer = 0f; // 대기 타이머

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) 
        : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();

    }

}