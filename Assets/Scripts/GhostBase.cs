using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "Ghost/Create new Ghost")]

public class GhostBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite bigSprite;
    [SerializeField] Sprite smallSprite;

    [SerializeField] GhostType type;


    //combat information
    [SerializeField] int damage;
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

}

public enum GhostType
{
    Red,
    Blue,
    Green,
}

public class TypeChart
{
    float[][] chart =
    {
      //                        RED BLU GRE
      /* RED */    new float[] { 1f, 0.5f, 2f},
      /* BLUE */   new float[] { 2f, 1f, 0.5f},
      /* GREEN */  new float[] { 0.5f, 2f, 1f},
    };

}