using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostBase_KatieVer : MonoBehaviour
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

    private BattleSystem_Integrate BattleSystem_Integrate;
    private Vector3 originalScale;

    private void Start()
    {
        BattleSystem_Integrate = FindObjectOfType<BattleSystem_Integrate>();
        originalScale = transform.localScale;

    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.2f;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        if (BattleSystem_Integrate != null)
        {
            if (skill == GhostSkill.Attack)
            {
                Debug.Log("YOU CLICKED A GHOST");
                switch (type)
                {
                    case GhostType.Red:
                        BattleSystem_Integrate.OnAttackButtonRed(skillAmount);
                        break;
                    case GhostType.Blue:
                        BattleSystem_Integrate.OnAttackButtonBlue(skillAmount);
                        break;
                    case GhostType.Green:
                        BattleSystem_Integrate.OnAttackButtonGreen(skillAmount);
                        break;
                    default:
                        Debug.LogWarning("Unhandled GhostType in switch statement");
                        break;
                }
            }
            else if (skill == GhostSkill.Heal)
            {
                BattleSystem_Integrate.OnHealButton(skillAmount);
            }
        }
    }
}