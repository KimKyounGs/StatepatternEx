using UnityEngine;

public class PlayerWallSlideState : PlayerState
{

    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        rb.linearVelocity = new Vector2(0, -player.wallSlideSpeed);
        
        if (player.IsWallDetected()) // 벽에 계속 닿아져야 함.
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector2(-player.facingDir * player.wallJumpForce, player.wallJumpForce);
                player.Flip();
                stateMachine.ChangeState(player.jumpState);
            }
        }
        else // 아니면 떨어짐.
        {
            stateMachine.ChangeState(player.airState);
        }

        // 땅에 닿으면
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
