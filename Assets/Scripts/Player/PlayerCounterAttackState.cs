using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfullCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();


        player.ZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius); 

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfullCounterAttack", true);
                }
            }
        }

        // triggerCalled 애니메이션이 끝이 나면 
        if (stateTimer < 0 || triggerCalled) stateMachine.ChangeState(player.idleState);
        
    }
    
    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("SuccessfullCounterAttack", false);
    }
}
