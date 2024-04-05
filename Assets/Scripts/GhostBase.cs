using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBase : MonoBehaviour
{

    public Ghost ghost;

    // Ghost Combat info

    public enum GhostType { Red, Blue, Green }
    [SerializeField] GhostType type;

    public enum GhostSkill { Attack, Heal }
    [SerializeField] GhostSkill skill;

    // Combat information
    [SerializeField] int skillAmount;

    private BattleSystem_Integrate2 battleSystem;
    private Vector3 originalScale;

    // Cooldown variables
    float cooldownDuration = 1f;
    private bool isCooldown = false;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem_Integrate2>();
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.3f;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        if (!isCooldown && battleSystem.state == BattleState.PLAYERTURN)
        {
            StartCoroutine(ActivateCooldown());
            UseSkill();
        }
    }

    private void UseSkill()
    {
        if (battleSystem != null)
        {
            if (skill == GhostSkill.Attack)
            {
                Debug.Log("YOU CLICKED A GHOST");
                switch (type)
                {
                    case GhostType.Red:
                        battleSystem.OnAttackButtonRed(skillAmount);
                        break;
                    case GhostType.Blue:
                        battleSystem.OnAttackButtonBlue(skillAmount);
                        break;
                    case GhostType.Green:
                        battleSystem.OnAttackButtonGreen(skillAmount);
                        break;
                    default:
                        Debug.LogWarning("Unhandled GhostType in switch statement");
                        break;
                }
            }
            else if (skill == GhostSkill.Heal)
            {
                battleSystem.OnHealButton(skillAmount);
            }
        }
    }

    private IEnumerator ActivateCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
    }
}
