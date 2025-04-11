using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttack(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        if (xInput != 0)
        {
            Debug.Log(xInput);
            attackDir = xInput;
        }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f);
        // player.anim.speed = 1;

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) player.ZeroVelocity();

        if (triggerCalled) stateMachine.ChangeState(player.idleState);
    }
}