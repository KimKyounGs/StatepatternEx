using System.Buffers.Text;
using UnityEngine;

public class DashSkill : Skill
{

    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("오버라이드 함수 UseSkill");
    }
}
