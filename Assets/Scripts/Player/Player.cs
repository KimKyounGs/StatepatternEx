using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Player : Entity
{
    [Header("공격 디테일")]
    public Vector2[] attackMovement = new Vector2[3]; // 공격 시 이동 방향
    public float counterAttackDuration = 0.2f;


    public SkillManager skill { get; private set;}
    public GameObject sword { get; private set; }
    public bool isBusy { get; private set; }
    public float swordReturnImpact;

    [Header("플레이어 정보")] 
    public float moveSpeed = 12f;
    public float jumpForce;
    public float wallSlideSpeed;
    public float wallJumpForce;

    [Header("대시 정보")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }



    #region States

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerPrimaryAttack primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerAimSwordState aimSword {get; private set;}
    public PlayerCatchSwordState catchSword { get; private set; }
    #endregion
    

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void ClearTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
       
        yield return new WaitForSeconds(_seconds);
      
        isBusy = false;
    }


    private void CheckForDashInput()
    {
        if (IsWallDetected()) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
           
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger(); // 애니메이션 트리거 호출
    

}
