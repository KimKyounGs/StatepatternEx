using Unity.VisualScripting;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{   
    EnemySkeleton enemy;
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) 
        : base(_enemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();



    }
}