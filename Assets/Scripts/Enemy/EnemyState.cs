using UnityEngine;

public class EnemyState : MonoBehaviour
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    private string animBoolName;

    protected Rigidbody2D rb;
    protected float xValue;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);
        rb = enemy.rb;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

}
