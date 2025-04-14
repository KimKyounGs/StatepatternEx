using TreeEditor;
using Unity.Mathematics;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("스킬 정보")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir; 
    [SerializeField] private float swordGravity;


    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        newSwordScript.SetupSword(launchDir, swordGravity);
    }

}
