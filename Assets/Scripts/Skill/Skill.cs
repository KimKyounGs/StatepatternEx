using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            // 스킬 사용
            cooldownTimer = cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");

        return false;
    }

    public virtual void UseSkill()
    {
        //스킬사용
    }

}
