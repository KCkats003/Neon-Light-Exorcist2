using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;

        // Get the current health from the unit
        int currentHP = unit.currentHP;
        hpSlider.value = currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
