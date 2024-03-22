using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostBase : MonoBehaviour 
{

    //Ghost Combat info
    /*
     * Red > Green
     * Green > Blue
     * Blue > Red
     */
    public enum GhostType { Red, Blue, Green }
    [SerializeField] GhostType type;

    public enum GhostSkill { Attack, Heal }
    [SerializeField] GhostSkill skill;

    //combat information
    [SerializeField] int skillAmount;

    private BattleSystem battleSystem;
    private Vector3 originalScale;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        originalScale = transform.localScale;

    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.8f;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        if (battleSystem != null)
        {
            if (skill == GhostSkill.Attack)
            {
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
}