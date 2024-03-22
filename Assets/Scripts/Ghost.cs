using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "Ghost / Create new Ghost")]

public class Ghost : ScriptableObject
{
    //Ghost display info
    public int id;
    public string ghostName;
    public Sprite iconMini;
    public Sprite iconLarge;

    public string type;

    public string ability;

    [TextArea(3, 10)]
    public string description;
    public string skillAmount;
}
