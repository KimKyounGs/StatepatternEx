using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] private float cloneDuration;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1,1,1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
        }
    }
    public void SetupClone(Transform _newTransform, float _cloneDuration,bool _canAttack)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;
    }

    private void AnimationTrigger()
    {
        cloneTimer -= 0.1f;
    }

    private void AttackTrigger()
    {
        // Collider2D colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        // foreach(var in colliders)
        // {

        // }
    }
}
