using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    [SerializeField] private float returnSpeed = 12;
    private bool canRotate;
    private bool isReturning;

    [Header("관통 정보")]
    [SerializeField] private float pierceAmount;


    [Header("바운스 정보")]
    [SerializeField] private float bounceSpeed;
    private bool isBouncing = true;
    private int amountOfBounce;
    public List<Transform> enemyTarget;
    private int targetIndex;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();   
    }

    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;

        rb.linearVelocity = _dir;
        rb.gravityScale = _gravityScale;

        anim.SetBool("Rotation", true);
    }

    public void SetupBounce(bool _isBouncing, int _amountOfBounce)
    {
        isBouncing = _isBouncing;
        amountOfBounce = _amountOfBounce;

        enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }


    public void ReturnSword()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate) 
            transform.right = rb.linearVelocity;

        if (isReturning) 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }

        BounceLogic();
    }

    private void BounceLogic()
    {
        if(isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
        
            if(Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            {
                targetIndex++;
                amountOfBounce--;

                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning) 
            return;

        collision.GetComponent<Enemy>()?.Damage();
        

        if (collision.GetComponent<Enemy>() != null)
        {
            if(isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach(var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.transform);
                }
            }
        }
        
        StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // 움직임과 회전을 모두 고정(freeze) 

        if (isBouncing)
            return;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
